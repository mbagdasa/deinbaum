using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.BaumStruktur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace deinBaum.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BaumZustandController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public BaumZustandController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
      

        /// <summary>
        /// Gibt eine Liste von Baumzustände zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BaumZustand>>> Get()
        {
            return Ok(await _context.BaumZustand.ToListAsync());
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit dem ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<List<BaumZustand>>> Get(int id)
        {
            var newList = await _context.BaumZustand.FindAsync(id);

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {id}!");
            }
            return Ok(newList);
        }

        /// <summary>
        ///  Gibt eine Liste mit dem Suchbegriff zurück
        /// </summary>
        /// <param name="zustand"></param>
        /// <returns></returns>
        [HttpGet("zustand/{zustand}")]
        public async Task<ActionResult<List<BaumZustand>>> Get(string zustand)
        {
            var newList = await _context.BaumZustand.
                Where(entry => entry.Zustand.ToLower().Contains(zustand.ToLower())).ToListAsync();

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {zustand}!");
            }
            return Ok(newList);
        }


        /// <summary>
        /// Fügt ein Baumzustand hinzu
        /// </summary>
        /// <param name="zustand"></param>
        /// <returns></returns>
        [HttpPost("addZustand/{zustand}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumZustand>>> AddZustand(string zustand)
        {
            BaumZustandDTO dto = _context.BaumZustand.OrderBy(x => x.ID).Last();
            dto.Zustand = zustand;
            dto.ID++;
            await _context.BaumZustand.AddAsync(dto);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumZustand.ToListAsync());
        }

        /// <summary>
        /// Fügt ein Baumzustand in der DB hinzu
        /// </summary>
        /// <param name="zustand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumZustand>>> AddZustand(BaumZustand zustand)
        {
            BaumZustandDTO dto = _mapper.Map<BaumZustandDTO>(zustand);
            await _context.BaumZustand.AddAsync(dto);
            await _context.SaveChangesAsync();
            return Ok(await _context.BaumZustand.ToListAsync());
        }

        
        /// <summary>
        /// Löscht ein Baumzustand aus der DB
        /// </summary>
        /// <param name="baumZustand"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumZustand>>> DeleteZustand(int Id)
        {
            var zustand =  _context.BaumZustand.Where(x => x.ID == Id).FirstOrDefault();
            if (zustand is null)
                return BadRequest("baumZustand not found");

            _context.BaumZustand.Remove(zustand);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumZustand.ToListAsync());
        }


        /// <summary>
        /// Aktualisiert ein Baumzustand
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateBaumZustand/zustand/{alterZustand}/{neuerZustand}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumZustand>>> UpdateArt(string zustand, string newZustand)
        {
            List<BaumZustandDTO> zustandList = _context.BaumZustand
               .Where(entry => entry.Zustand.ToLower().Equals(zustand.ToLower()))
               .Distinct()
               .Take(1).ToList();
            if (zustandList is null || zustandList.Count <= 0)
                return BadRequest("BaumMerkmal not found");

            zustandList[0].Zustand = newZustand;

            _context.BaumZustand.Update(zustandList[0]);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumZustand.ToListAsync());
        }


        /// <summary>
        /// Aktualisiert ein Baumzustand
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateBaumZustand/id/{id}/{neuerZustand}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumZustand>>> UpdateArt(int id, string newZustand)
        {
            var zustand = await _context.BaumZustand.FindAsync(id);
            if (zustand is null)
                return BadRequest("BaumMerkmal not found");


            zustand.Zustand = newZustand;

            _context.BaumZustand.Update(zustand);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumZustand.ToListAsync());
        }

    }
}
