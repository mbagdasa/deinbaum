using AutoMapper;
using deinBaum.DAL.Model;
using deinBaum.Lib.BaumStruktur;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace deinBaum.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaumMerkmalController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public BaumMerkmalController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gibt eine Liste von Baummerkmalen zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<BaumMerkmal>>> Get()
        {
            return Ok(await _context.BaumMerkmal.ToListAsync());
        }

        /// <summary>
        ///  Gibt eine Liste von Treffern mit dem ID zurück
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("id/{id}")]
        public async Task<ActionResult<List<BaumMerkmal>>> Get(int id)
        {
            var newList = await _context.BaumMerkmal.FindAsync(id);

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {id}!");
            }
            return Ok(newList);
        }

        /// <summary>
        ///  Gibt eine Liste mit dem Suchbegriff zurück
        /// </summary>
        /// <param name="merkmal"></param>
        /// <returns></returns>
        [HttpGet("merkmal/{merkmal}")]
        public async Task<ActionResult<List<BaumMerkmal>>> Get(string merkmal)
        {
            var newList = await _context.BaumMerkmal.
                Where(entry => entry.Merkmal.ToLower().Contains(merkmal.ToLower())).ToListAsync();

            if (newList is null)
            {
                return BadRequest($"Kein Treffer zum Suchbegriff: {merkmal}!");
            }
            return Ok(newList);
        }

        /// <summary>
        /// Fügt ein Baummerkmal in der DB hinzu
        /// </summary>
        /// <param name="merkmal"></param>
        /// <returns></returns>
        [HttpPost("addMerkmal/{merkmal}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumMerkmal>>> AddMerkmal(string merkmal)
        {
            BaumMerkmalDTO dto = _context.BaumMerkmal.OrderBy(x => x.ID).Last();
            dto.Merkmal = merkmal;
            dto.ID++;
            await _context.BaumMerkmal.AddAsync(dto);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumMerkmal.ToListAsync());
        }

        /// <summary>
        /// Fügt ein Baummerkmal in der DB hinzu
        /// </summary>
        /// <param name="merkmal"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumMerkmal>>> AddMerkmal(BaumMerkmal merkmal)
        {
            BaumMerkmalDTO dto = _mapper.Map<BaumMerkmalDTO>(merkmal);
            await _context.BaumMerkmal.AddAsync(dto);
            await _context.SaveChangesAsync();
            return Ok(await _context.BaumMerkmal.ToListAsync());
        }

       
        /// <summary>
        /// Löscht das Baummerkmal aus der DB
        /// </summary>
        /// <param name="baumMerkmal"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumMerkmal>>> DeleteMerkmal(int Id)
        {
            var merkmal =  _context.BaumMerkmal.Where(x => x.ID ==Id).FirstOrDefault();
            if (merkmal is null)
                return BadRequest("BaumMerkmal not found");

            _context.BaumMerkmal.Remove(merkmal);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumMerkmal.ToListAsync());
        }

        
        /// <summary>
        /// Aktualisiert ein Baummerkmal
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateBaumMerkmal/merkmal/{alterMerkmal}/{neuerMerkmal}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumMerkmal>>> UpdateArt(string merkmal, string newMerkmal)
        {
            List<BaumMerkmalDTO> merkmalList = _context.BaumMerkmal
               .Where(entry => entry.Merkmal.ToLower().Equals(merkmal.ToLower()))
               .Distinct()
               .Take(1).ToList();
            if (merkmalList is null || merkmalList.Count <= 0)
                return BadRequest("BaumMerkmal not found");

            merkmalList[0].Merkmal = newMerkmal;

            _context.BaumMerkmal.Update(merkmalList[0]);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumMerkmal.ToListAsync());
        }


        /// <summary>
        /// Aktualisiert ein Baummerkmal
        /// </summary>
        /// <param name="artName"></param>
        /// <returns></returns>
        [HttpPut("updateBaumMerkmal/id/{id}/{neuerMerkmal}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<BaumMerkmal>>> UpdateArt(int id, string neuerMerkmal)
        {
            var merkaml = await _context.BaumMerkmal.FindAsync(id);
            if (merkaml is null)
                return BadRequest("BaumMerkmal not found");


            merkaml.Merkmal = neuerMerkmal;

            _context.BaumMerkmal.Update(merkaml);
            await _context.SaveChangesAsync();

            return Ok(await _context.BaumMerkmal.ToListAsync());
        }

    }
}
