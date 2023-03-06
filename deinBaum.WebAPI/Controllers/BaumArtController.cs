using deinBaum.Lib.BaumStruktur;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.IO;
using AutoMapper;
using deinBaum.DAL.Model;
using Microsoft.AspNetCore.Authorization;

namespace deinBaum.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaumArtController : ControllerBase
    {
       private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public BaumArtController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        /////////////////////////
        ///                   ///
        ///        GET        ///
        ///                   ///
        /////////////////////////


        /// <summary>
        /// Gibt eine Liste aller Baumarten zurück
        /// </summary>
        /// <returns>List<BaumArt></returns>
        [HttpGet]
        public async Task<ActionResult<List<BaumArt>>> Get()
        {
            return Ok(await _context.BaumArt.ToListAsync());
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit dem ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<List<BaumArt>>> Get(int id)
        {
            var newList = await _context.BaumArt.FindAsync(id);

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {id}!");
            }
            return Ok(newList);
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit dem Suchbegriff zurück
        /// </summary>
        /// <param name="art"></param>
        /// <returns></returns>
        [HttpGet("art/{art}")]
        public async Task<ActionResult<List<BaumArt>>> Get(string art)
        {
            var newList = await _context.BaumArt.
                Where(entry => entry.Art.ToLower().Contains(art.ToLower())).ToListAsync();

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {art}!");
            }
            return Ok(newList);
        }



        /////////////////////////
        ///                   ///
        ///        Post       ///
        ///                   ///
        /////////////////////////

        /// <summary>
        /// Baumart hinzufügen mit dem Parameter Artname
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPost("addBaumArt/{art}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumArt>>> AddArt(string artName)
        {
            BaumArtDTO dto = _context.BaumArt.OrderBy(x => x.ID).Last();
            dto.Art = artName;
            dto.ID++;
            //BaumArtDTO addBaumDTO = new()
            //{
            //    ID = dto.ID++,
            //    Art = artName
            //};
            await _context.BaumArt.AddAsync(dto);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumArt.ToListAsync());
        }

        /// <summary>
        /// Fügt eine BaumArt Objekt in der DB ein
        /// </summary>
        /// <param name="baumArt"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumArt>>> AddArt(BaumArt baumArt)
        {
            BaumArtDTO dto = _mapper.Map<BaumArtDTO>(baumArt);
            await _context.BaumArt.AddAsync(dto);
            await _context.SaveChangesAsync();
            return Ok(await _context.BaumArt.ToListAsync());
        }


        /////////////////////////
        ///                   ///
        ///      Delete       ///
        ///                   ///
        /////////////////////////

        /// <summary>
        ///  Löscht das Baumart in der DB
        /// </summary>
        /// <param name="baumArt"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumArt>>> DeleteArt(int Id)
        {

            var art =  _context.BaumArt.Where(x =>x.ID ==Id).FirstOrDefault();
            if (art is null)
                return BadRequest("Baumart not found");

            _context.BaumArt.Remove(art);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumArt.ToListAsync());
        }

        /////////////////////////
        ///                   ///
        ///         put       ///
        ///                   ///
        /////////////////////////

        /// <summary>
        /// Die Artname wird aktualisiert
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateBaumArt/artName/{alterArtName}/{neuerArtName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumArt>>> UpdateArt(string artName, string newArtName)
        {
            List<BaumArtDTO> artList = _context.BaumArt
               .Where(entry => entry.Art.ToLower().Equals(artName.ToLower()))
               .Distinct()
               .Take(1).ToList();
            if (artList is null || artList.Count <= 0)
                return BadRequest("Baumart not found");

            artList[0].Art = newArtName;

            _context.BaumArt.Update(artList[0]);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumArt.ToListAsync());
        }


        /// <summary>
        /// Die Artname wird mittels ID aktualisiert
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateBaumArt/id/{id}/{neuerArtName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumArt>>> UpdateArt(int id, string newArtName)
        {
            var art = await _context.BaumArt.FindAsync(id);
            if (art is null)
                return BadRequest("Baumart not found");


            art.Art = newArtName;

            _context.BaumArt.Update(art);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumArt.ToListAsync());
        }
    }
}
