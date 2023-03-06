using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.PersonDaten;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace deinBaum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class WaldeigentuemerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public WaldeigentuemerController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /// <summary>
        /// Gibt eine Liste von Waldeigentümer zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Waldeigentuemer>>> Get()
        {
            return Ok(await _context.Waldeigentuemer.ToListAsync());
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit dem ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<List<Waldeigentuemer>>> Get(int id)
        {
            var newList = await _context.Waldeigentuemer.FindAsync(id);

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {id}!");
            }
            return Ok(newList);
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit der Emailadresse zurück
        /// </summary>
        /// <param name="kuerzel"></param>
        /// <returns></returns>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Waldeigentuemer>> Get(string email)
        {
            var newList =  _context.Waldeigentuemer.Where(x=> x.Email.ToLower().Equals(email.ToLower())).FirstOrDefault();

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {email}!");
            }
            return Ok(newList);
        }

        /// <summary>
        /// Gibt ein boolean zurück, ob der Wladeigentümer existiert (true) oder nicht (false)
        /// </summary>
        /// <returns></returns>
        [HttpGet("existsWaldeigentuemer/{email}")]
        public async Task<ActionResult<bool>> GetLogin(string email)
        {
            var record = _context.Waldeigentuemer.Where(x => x.Email.ToLower().Equals(email.ToLower().Trim())).FirstOrDefault();

            if (record is null)
            {
                return Ok(false);
            }
            return Ok(true);
        }

        /// <summary>
        /// Fügt ein Eigentümer hinzu
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<Waldeigentuemer>>> AddEigentümer(Waldeigentuemer mitarbeiter)
        {
            WaldeigentuemerDTO dto = _mapper.Map<WaldeigentuemerDTO>(mitarbeiter);

            WaldeigentuemerDTO? existsRecord = _context.Waldeigentuemer.Where(x => x.Email.ToLower().Equals(dto.Email.ToLower().Trim())).FirstOrDefault();
            if (existsRecord is not null)
            {
                if (!string.IsNullOrEmpty(existsRecord.Email)
                    && existsRecord.Email.ToLower().Equals(dto.Email.ToLower()))
                {
                    return BadRequest($"Waldeigentümer mit der Email={dto.Email} existiert schon in der Datenbank");
                }
            }
            await _context.Waldeigentuemer.AddAsync(dto);
            await _context.SaveChangesAsync();
            return Ok(await _context.Waldeigentuemer.ToListAsync());
        }

        /// <summary>
        /// Löscht ein Waldeigentümer aus der DB
        /// </summary>
        /// <param name="eigentuemer"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Waldeigentuemer>>> DeleteWaldeigentuemer(int Id)
        {
            var record = _context.Waldeigentuemer.Where(x => x.ID.Equals(Id)).FirstOrDefault();
            if (record is null)
                return BadRequest("Waldeigentuemer existiert nicht");
                _context.Waldeigentuemer.Remove(record);
                await _context.SaveChangesAsync();
            return Ok(await _context.Waldeigentuemer.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateWaldeigentuemer/id/{id}")]
        public async Task<ActionResult<List<Waldeigentuemer>>> UpdateWaldeigentuemer(int id, Waldeigentuemer updatedWaldeigentuemer)
        {
            WaldeigentuemerDTO dto = _mapper.Map<WaldeigentuemerDTO>(updatedWaldeigentuemer);
            var record =  _context.Waldeigentuemer.Where(x => (x.ID == id)).AsNoTracking().FirstOrDefault();
            if(record is not null)
            {
                //Überprüfen ob Login Name exisitert
                var existsRecord = _context.Waldeigentuemer.AsNoTracking().Where(x => x.Email.ToLower().Equals(record.Email.ToLower().Trim())
                                 && x.ID != record.ID) // und nicht derselbe ID
                            .FirstOrDefault();
                if (existsRecord is null)
                {
                    //Login soll nicht überschrieben werden
                    dto.Email = record.Email;
                    _context.Waldeigentuemer.Update(dto);
                    await _context.SaveChangesAsync();
                    return Ok(await _context.Waldeigentuemer.ToListAsync());
                }
                else
                {
                    return BadRequest("Email Adresse darf nicht verändert werden");
                }
            }

            return BadRequest("Waldeigentümer existiert nicht");
            
        }

        /// <summary>
        ///  Feldmitarbeiter aktualisieren mittels loginname
        /// </summary>
        /// <param name="email"></param>
        /// <param name="updatedFeldmitarbeiter"></param>
        /// <returns></returns>
        [HttpPut("updateWaldeigentuemer/email/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Waldeigentuemer>>> Update(string email, Waldeigentuemer updatedFeldmitarbeiter)
        {
            WaldeigentuemerDTO dto = _mapper.Map<WaldeigentuemerDTO>(updatedFeldmitarbeiter);

            var waldeigentuemerExistiert = _context.Feldmitarbeiter.AsNoTracking().Where(x => x.Login.ToLower().Equals(email.ToLower().Trim())).FirstOrDefault();

            if (waldeigentuemerExistiert is not null)
            {
                dto.Email = email;
                _context.Waldeigentuemer.Update(dto);
                await _context.SaveChangesAsync();

                return Ok(await _context.Waldeigentuemer.ToListAsync());
            }

            return BadRequest("Waldeigentümer existiert nicht");

        }
    }
}
