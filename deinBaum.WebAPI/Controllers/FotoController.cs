using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.FotoStruktur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace deinBaum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        [Authorize]
    public class FotoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public FotoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gibt eine Liste mit Fotos zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Foto>>> Get()
        {
            return Ok(await _context.Foto.ToListAsync());
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit dem ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<List<Foto>>> Get(int id)
        {
            var newList = await _context.Foto.FindAsync(id);

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {id}!");
            }
            return Ok(newList);
        }

        /// <summary>
        ///  Gibt eine Liste mit dem Suchbegriff zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<Foto>>> Get(string name)
        {
            var newList = await _context.Foto.
                Where(entry => entry.Name.ToLower().Contains(name.ToLower())).ToListAsync();

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {name}!");
            }
            return Ok(newList);
        }
                
        /// <summary>
        /// Fügt ein Foto in der DB hinzu
        /// </summary>
        /// <param name="foto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<List<Foto>>> AddFoto(Foto foto)
        {
            FotoDTO dto = _mapper.Map<FotoDTO>(foto);
            await _context.Foto.AddAsync(dto);
            await _context.SaveChangesAsync();
            return Ok(await _context.Foto.ToListAsync());
        }

        /// <summary>
        /// Löscht ein Foto aus der DB
        /// </summary>
        /// <param name="fotoObject"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Foto>>> DeleteFoto(int Id)
        {
            var foto =  _context.Foto.Where(x => x.ID.Equals(Id)).FirstOrDefault();
            if (foto is null)
                return BadRequest("Foto not found");

            _context.Foto.Remove(foto);
            await _context.SaveChangesAsync();

            return Ok(await _context.Foto.ToListAsync());
        }

    }
}
