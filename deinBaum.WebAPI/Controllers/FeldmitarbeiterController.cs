using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.PersonDaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata;

namespace deinBaum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeldmitarbeiterController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public FeldmitarbeiterController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        ///  Gibt eine Liste von Feldmitarbeiter zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Feldmitarbeiter>>> Get()
        {
            return Ok(await _context.Feldmitarbeiter.ToListAsync());
        }

        /// <summary>
        /// Gibt ein Feldmitarbeiter mit dem Login zurück, Wenn keine  existiert gibt er einen leeren Feldmitarbeiter Objekt mit
        /// </summary>
        /// <returns></returns>
        [HttpGet("login/{login}")]
        public async Task<ActionResult<Feldmitarbeiter>> Get(string login)
        {
            FeldmitarbeiterDTO? record = _context.Feldmitarbeiter.Where(x => x.Login.ToLower().Equals(login.ToLower().Trim())).FirstOrDefault();

            if(record is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {login}!");
            }
            return Ok(record);
        }

        /// <summary>
        /// Gibt ein boolean zurück, ob der User existiert (true) oder nicht (false)
        /// </summary>
        /// <returns></returns>
        [HttpGet("existsLogin/{login}")]
        public async Task<ActionResult<bool>> GetLogin(string login)
        {
            var record = _context.Feldmitarbeiter.Where(x => x.Login.ToLower().Equals(login.ToLower().Trim())).FirstOrDefault();

            if( record is null)
            {
                return Ok(false);
            }
            return Ok(true);
        }

        /// <summary>
        ///  Gibt ein Feldmitarbeiter mit dem ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Feldmitarbeiter>> Get(int id)
        {

            var newList = await _context.Feldmitarbeiter.FindAsync(id);

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {id}!");
            }
            return Ok(newList);
        }

        /// <summary>
        /// Wenn der Feldmitarbeiter nicht Login existiert, wird ein Feldmitarbeiter angelegt
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Feldmitarbeiter>>> Add(Feldmitarbeiter mitarbeiter)
        {
            FeldmitarbeiterDTO dto = _mapper.Map<FeldmitarbeiterDTO>(mitarbeiter);

            FeldmitarbeiterDTO? existsRecord = _context.Feldmitarbeiter.Where(x => x.Login.ToLower().Equals(dto.Login.ToLower().Trim())).FirstOrDefault();
            if (existsRecord is not null)
            {
                if (!string.IsNullOrEmpty(existsRecord.Login)
                    && existsRecord.Login.ToLower().Equals(dto.Login.ToLower()))
                {
                    return BadRequest($"Feldmitarbeiter mit dem Login={dto.Login} existiert schon in der Datenbank");
                }
            }

            await _context.Feldmitarbeiter.AddAsync(dto);
            await _context.SaveChangesAsync();
            return Ok(await _context.Feldmitarbeiter.ToListAsync());
        }


        /// <summary>
        ///  Löscht den Feldmitarbeiter mit der ID
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Feldmitarbeiter>>> DeleteLogin(string login)
        {
            var mitarbeiterResult =  _context.Feldmitarbeiter.Where(x => x.Login.ToLower().Equals(login.ToLower().Trim())).FirstOrDefault();
            if (mitarbeiterResult is null)
                return BadRequest("Feldmitarbeiter existiert nicht");

            var logindataResult = _context.User.Where(x => x.Login.ToLower().Equals(login.ToLower())).FirstOrDefault();
            if (logindataResult is not null)
            {
                _context.Feldmitarbeiter.Remove(mitarbeiterResult);
                //await _context.SaveChangesAsync();

                //_context.User.Remove(logindataResult);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Feldmitarbeiter ist nicht mit einem User-Entity verknüpft");
            }

            return Ok(await _context.Feldmitarbeiter.ToListAsync());
        }


        /// <summary>
        ///  Feldmitarbeiter aktualisieren mittels id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedFeldmitarbeiter"></param>
        /// <returns></returns>
        [HttpPut("updateFeldmitarbeiter/id/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Feldmitarbeiter>>> UpdateFeldmitarbeiter(int id, Feldmitarbeiter updatedFeldmitarbeiter)
        {
            FeldmitarbeiterDTO dto = _mapper.Map<FeldmitarbeiterDTO>(updatedFeldmitarbeiter);
            
            var mitarbeiterExistiert =  _context.Feldmitarbeiter.Where(x=> (x.ID==id)).AsNoTracking().FirstOrDefault();

            if (mitarbeiterExistiert is not null)
            {
                
                //Überprüfen ob Login Name exisitert
                var record = _context.Feldmitarbeiter.AsNoTracking().Where(x => x.Login.ToLower().Equals(updatedFeldmitarbeiter.Login.ToLower().Trim()) 
                                 &&  x.ID != updatedFeldmitarbeiter.ID) // und nicht derselbe ID
                            .FirstOrDefault();
                if (record is null)
                {
                    //Login soll nicht überschrieben werden
                    dto.Login = mitarbeiterExistiert.Login;
                    _context.Feldmitarbeiter.Update(dto);
                    await _context.SaveChangesAsync();

                    return Ok(await _context.Feldmitarbeiter.ToListAsync());
                }
                else
                {
                    return BadRequest("Loginname ist bereits vergeben");

                }
            }
            return BadRequest("Feldmitarbeiter existiert nicht");
        }

        /// <summary>
        ///  Feldmitarbeiter aktualisieren mittels loginname
        /// </summary>
        /// <param name="login"></param>
        /// <param name="updatedFeldmitarbeiter"></param>
        /// <returns></returns>
        [HttpPut("updateFeldmitarbeiter/login/{login}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Feldmitarbeiter>>> Update(string login, Feldmitarbeiter updatedFeldmitarbeiter)
        {
            FeldmitarbeiterDTO dto = _mapper.Map<FeldmitarbeiterDTO>(updatedFeldmitarbeiter);

            var loginExistiert = _context.Feldmitarbeiter.AsNoTracking().Where(x => x.Login.ToLower().Equals(login.ToLower().Trim())).FirstOrDefault();

            if (loginExistiert is not null)
            {
                dto.Login = login;
                _context.Feldmitarbeiter.Update(dto);
                await _context.SaveChangesAsync();

                return Ok(await _context.Feldmitarbeiter.ToListAsync());
            }

            return BadRequest("Feldmitarbeiter existiert nicht");

        }
    }
}
