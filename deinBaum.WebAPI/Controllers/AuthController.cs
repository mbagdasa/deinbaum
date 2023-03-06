using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.PersonDaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using deinBaum.WebAPI.Models;
using LoginResponse = deinBaum.Lib.PersonDaten.LoginResponse;

namespace deinBaum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(AppDbContext context, IMapper mapper, IConfiguration config, IUserService userService)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _userService = userService;
        }

        /// <summary>
        /// Registiert ein Login User für die App. Unterscheidet wird der Loginname für Admint mit dem suffix 'admin'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(User request)
        {
            UserDTO user = _mapper.Map<UserDTO>(request);

            //// überprüfen ob der Loginname schon existiert?
            if (_context.User.Count() != 0)
            {
                var existsUser = await _context.User.FindAsync(request.Login);
                if (existsUser != null)
                {
                    // Wenn existiert
                    return BadRequest("Loginname ist schon vergeben");
                }
            }
            //Password Wird verschlüsselt und Hash erzeugt
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Login = request.Login;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            // Admin Rolle überwiesen, wenn im Name admin vorhanden ist
            if (user.Login.ToLower().Contains("admin"))
            {
                user.IstAdminBerechtigt = true;
            }

            user.IstUserAktiv = true;

            //RefreshToken erzeugt
            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken, user);

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }


        /// <summary>
        /// Login Prozess mit dem Login Credentials
        /// </summary>
        /// <param name="request"> User Object mit Login und Password</param>
        /// <returns>Token</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User request)
        {
            if (_context.User.Count() == 0)
            {
                return BadRequest("User not found!");
            }

            // Ueberprueft ob der User in der DB schon existiert
            UserDTO user = new();
            try
            {
                user = _context.User.Where(x => x.Login.Equals(request.Login) && x.IstUserAktiv).First();
                if (user is null)
                {
                    return BadRequest("User not found!");
                }
            }
            catch
            {
                return BadRequest("User not found!");
            }


            // Überprüft das Password mit dem PasswordHash and Salt, Wenn die Überprüfung fehl schlägt
            // erhält man ein Badrequest
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is not correct!");
            }

            //Token wird erstellt
            var token = CreateToken(user);

            //Refresh Token wird erstellt
            var refreshToken = new RefreshToken
            {
                Token = user.RefreshToken,
                ErstelltAm = user.TokenErstelltAm,
                LaeuftAbAm = user.TokenLaeuftAbAm
            };
            SetRefreshToken(refreshToken, user);

            var response = new LoginResponse()
            {
                Token = token,
                IstAdminBerechtigt = user.IstAdminBerechtigt
            };

            return Ok(response);
        }


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken(User user)
        {
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            var refreshToken = Request.Cookies["refreshToken"];
            if (!userDTO.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token.");
            }
            else if (userDTO.TokenLaeuftAbAm < DateTime.Now)
            {
                return Unauthorized("Token expired");
            }

            string token = CreateToken(userDTO);
            var newrefreshToken = GenerateRefreshToken();
            SetRefreshToken(newrefreshToken, userDTO);

            _context.User.Update(userDTO);
            await _context.SaveChangesAsync();

            return Ok(token);

        }


        /// <summary>
        /// Gibt eine List von Usern  ohne Passwörter zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<List<User>> Get()
        {
            List<User> listOfUsers = new List<User>();

            foreach (var obj in _context.User)
            {
                listOfUsers.Add(new User()
                {
                    Login = obj.Login,
                });
            }
            return listOfUsers;
        }

        /// <summary>
        /// Ein User mit dem Loginname wird gelöscht
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(string login)
        {
            if (_context.User.Count() != 0)
            {
                var user = _context.User.Where(x => x.Login.ToLower().Equals(login.ToLower())).FirstOrDefault();
                if (user is not null)
                {
                    _context.User.Remove(user);
                    await _context.SaveChangesAsync();
                    return Ok(true);
                }
                else
                {
                    return BadRequest("User nicht gefunden");
                }
            }
            return BadRequest("Userliste ist leer");

        }

        /// <summary>
        /// Refresh Token wird erzeugt
        /// </summary>
        /// <returns></returns>
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                LaeuftAbAm = DateTime.Now.AddDays(30),
                ErstelltAm = DateTime.Now
            };
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken, UserDTO user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.LaeuftAbAm
            };
            try
            {
                Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
            }
            finally
            {
                user.RefreshToken = newRefreshToken.Token;
                user.TokenErstelltAm = newRefreshToken.ErstelltAm;
                user.TokenLaeuftAbAm = newRefreshToken.LaeuftAbAm;
            }

        }

        /// <summary>
        /// Token für ein User wird erzeugt
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        private string CreateToken(UserDTO userDTO)
        {
            List<Claim> claims = new List<Claim>
            {
                //Man könnte hier auch Userrollen mitintegrieren
                new Claim(ClaimTypes.Name, userDTO.Login),
                new Claim(ClaimTypes.Role, userDTO.IstAdminBerechtigt ? "Admin":"Mitarbeiter")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(_config.GetSection("AuthSetting:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }


        /// <summary>
        /// Password Hash und Salt werden mithilfe eines Password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
