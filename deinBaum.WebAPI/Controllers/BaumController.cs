using AutoMapper;
using deinBaum.Lib.BaumStruktur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using deinBaum.DAL.Model;
using Microsoft.AspNetCore.Authorization;
using deinBaum.Lib.PersonDaten;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Linq;

namespace deinBaum.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaumController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public BaumController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        /// <summary>
        /// Gibt eine Liste aller Bäume aus der DB zurück
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<Baum>>> Get()
        {

            var baeume = _context.Baum.Include(e => e.BaumMerkmalRelation)
                                      .ThenInclude(ec => ec.Merkmal)
                                      .Include(a => a.BaumZustandRelation)
                                      .ThenInclude(z => z.Zustand)
                                      .Include(art => art.Art)
                                      .Include(f => f.Feldmitarbeiter)
                                      .Include(w => w.Waldeigentuemer)
                                      .Include(f => f.FotoListe);

            var returnValue = new List<Baum>();
            foreach (var b in baeume)
            {
                Baum baum = _mapper.Map<Baum>(b);
                foreach (var item in b.BaumMerkmalRelation)
                {
                    BaumMerkmal merkmal = _mapper.Map<BaumMerkmal>(item.Merkmal);

                    if (baum.Merkmale is null)
                        baum.Merkmale = new();

                    baum.Merkmale.Add(merkmal);
                    
                }

                foreach (var item in b.BaumZustandRelation)
                {
                    BaumZustand zustand = _mapper.Map<BaumZustand>(item.Zustand);

                    if (baum.ZustandsListe is null)
                        baum.ZustandsListe = new();
                    baum.ZustandsListe.Add(zustand);

                }

                returnValue.Add(baum);
            }

            return Ok(returnValue);
        }

        /// <summary>
        /// Gibt ein Baum zurück
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<List<Baum>>> Get(string name)
        {
            var baeume = _context.Baum.Include(e => e.BaumMerkmalRelation)
                                     .ThenInclude(ec => ec.Merkmal)
                                     .Include(a => a.BaumZustandRelation)
                                     .ThenInclude(z => z.Zustand)
                                     .Include(art => art.Art)
                                     .Include(f => f.Feldmitarbeiter)
                                     .Include(w => w.Waldeigentuemer)
                                     .Include(f => f.FotoListe).Where(x=> x.Name.ToLower().Equals(name.ToLower())).ToList();
            if (baeume is null)
            {
                return BadRequest("Baum nicht gefunden");
            }

            if(baeume.Count <= 0)
            {
                return BadRequest("Baum nicht gefunden");

            }

            var returnValue = new List<Baum>();
            foreach (var b in baeume)
            {
                Baum baum = _mapper.Map<Baum>(b);
                foreach (var item in b.BaumMerkmalRelation)
                {
                    BaumMerkmal merkmal = _mapper.Map<BaumMerkmal>(item.Merkmal);

                    if (baum.Merkmale is null)
                        baum.Merkmale = new();

                    baum.Merkmale.Add(merkmal);

                }

                foreach (var item in b.BaumZustandRelation)
                {
                    BaumZustand zustand = _mapper.Map<BaumZustand>(item.Zustand);

                    if (baum.ZustandsListe is null)
                        baum.ZustandsListe = new();
                    baum.ZustandsListe.Add(zustand);

                }

                returnValue.Add(baum);
            }

            return Ok(returnValue);

            
        }

        /// <summary>
        /// Gibt ein Boolean zurück ob der Baum existiert
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("exists/{name}")]
        public async Task<ActionResult<bool>> GetExistsBaum(string name)
        {
            var baum = _context.Baum.Include(e => e.BaumMerkmalRelation)
                                     .ThenInclude(ec => ec.Merkmal)
                                     .Include(a => a.BaumZustandRelation)
                                     .ThenInclude(z => z.Zustand)
                                     .Include(art => art.Art)
                                     .Include(f => f.Feldmitarbeiter)
                                     .Include(w => w.Waldeigentuemer)
                                     .Include(f => f.FotoListe).Where(x => x.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();

            if (baum is null)
            {
                return Ok(false);
            }
            return Ok(true);
        }


        /////////////////////////
        ///                   ///
        ///        Post       ///
        ///                   ///
        /////////////////////////

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baum"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<int>> AddBaum(Baum baum)
        {
            BaumDTO dto = _mapper.Map<BaumDTO>(baum);

            dto.ArtID = baum.Art.ID;
            dto.Art = null;

            dto.FeldmitarbeiterID = baum.Feldmitarbeiter.ID;
            dto.Feldmitarbeiter = null;

            dto.WaldeigentuemerID = baum.Waldeigentuemer.ID;
            dto.Waldeigentuemer = null;

            dto.BaumMerkmalRelation = null;
            dto.BaumZustandRelation = null;

            //Überprüfen ob ein Baum mit Baumname vorhanden ist
            BaumDTO? existsBaum = null;
            if (!string.IsNullOrEmpty(baum.Name))
            {
                existsBaum = _context.Baum.Where(x => x.Name.ToLower().Equals(baum.Name.ToLower())).FirstOrDefault();
            }
            

            if (existsBaum is null || string.IsNullOrEmpty(baum.Name))
            {
                _context.Baum.Add(dto);

                // Muss gespeichert werden um die aktuelle ID des Baumes zu erhalten
                await _context.SaveChangesAsync();

                var baumID = dto.ID;

                // Merkmal Relation befuellen
                if (baum.Merkmale is not null)
                {
                    foreach (var item in baum.Merkmale)
                    {
                        var rel = new BaumMerkmalRelationDTO() { BaumID = baumID, MerkmalID = item.ID };
                        _context.BaumMerkmalRelation.Add(rel);
                    }
                }

                // Zustand Relation befuellen
                if (baum.ZustandsListe is not null)
                {
                    foreach (var item in baum.ZustandsListe)
                    {
                        var rel = new BaumZustandRelationDTO() { BaumID = baumID, ZustandID = item.ID };
                        _context.BaumZustandRelation.Add(rel);
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(baumID);
            }
            else
            {
                return BadRequest("Baumname existiert bereits in der DB");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baum"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<int>> UpdateBaum(Baum requestBaum)
        {

            var dbBaum = _context.Baum.Where(x => x.ID == requestBaum.ID).AsNoTracking().FirstOrDefault();
            if (dbBaum is null)
            {
                return BadRequest("Baum nicht gefunden");
            }

            BaumDTO dto = _mapper.Map<BaumDTO>(requestBaum);

            dto.ArtID = requestBaum.Art.ID;
            dto.Art = null;

            dto.FeldmitarbeiterID = requestBaum.Feldmitarbeiter.ID;
            dto.Feldmitarbeiter = null;

            dto.WaldeigentuemerID = requestBaum.Waldeigentuemer.ID;
            dto.Waldeigentuemer = null;

            dto.BaumMerkmalRelation = null;
            dto.BaumZustandRelation = null;

            dto.FotoListe = null;
           

            if(dto != null)
            {
                //Zuerst den Baum DB updaten
                _context.Baum.Update(dto);
                await _context.SaveChangesAsync();
                var baumID = dto.ID;
                // Merkmal Relation befuellen
                if (requestBaum.Merkmale is not null)
                {
                    var listOfMerkmal = _context.BaumMerkmalRelation.Where(x => x.BaumID ==baumID).AsNoTracking().ToList<BaumMerkmalRelationDTO>();

                    // Entfernen
                    if(listOfMerkmal != null)
                    {
                        foreach (var item in listOfMerkmal)
                        {
                            _context.BaumMerkmalRelation.Remove(item);
                        }
                    }
                    //Hinzufügen
                    foreach (var item in requestBaum.Merkmale)
                    {
                        var rel = new BaumMerkmalRelationDTO() { BaumID = baumID, MerkmalID = item.ID };
                        _context.BaumMerkmalRelation.Add(rel);
                    }

                }

                // Zustand Relation befuellen
                if (requestBaum.ZustandsListe is not null)
                {
                    var listOfZustand = _context.BaumZustandRelation.Where(x => x.BaumID == baumID).AsNoTracking().ToList<BaumZustandRelationDTO>();

                    // Entfernen
                    if (listOfZustand != null)
                    {
                        foreach (var item in listOfZustand)
                        {
                            _context.BaumZustandRelation.Remove(item);
                        }
                    }
                    //Hinzufügen
                    foreach (var item in requestBaum.ZustandsListe)
                    {
                        var rel = new BaumZustandRelationDTO() { BaumID = baumID, ZustandID = item.ID };
                        _context.BaumZustandRelation.Add(rel);
                    }
                }

                // Fotos befuellen
                if (requestBaum.FotoListe is not null)
                {
                    var listOfFoto = _context.Foto.Where(x => x.BaumID == baumID).AsNoTracking().ToList<FotoDTO>();

                    // Entfernen
                    if (listOfFoto != null)
                    {
                        foreach (var item in listOfFoto)

                        {
                            _context.Foto.Remove(item);
                        }
                    }

                    await _context.SaveChangesAsync();

                    //Hinzufügen
                    foreach (var item in requestBaum.FotoListe)
                    {
                        var foto = new FotoDTO() { BaumID = baumID, Fotobytes = item.Fotobytes, Name = item.Name };
                        _context.Foto.Add(foto);

                    }
                }
            }

            else
            {

                return BadRequest("Updating fehlgeschlagen!");
            }

            await _context.SaveChangesAsync();
            return Ok(requestBaum.ID);
        }


        //TODO Delete nur für admin
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> Delete(int Id)
        {

            var dbBaum = _context.Baum.Where(x => x.ID == Id).AsNoTracking().FirstOrDefault();
            if (dbBaum is null)
            {
                return BadRequest("Baum nicht gefunden");
            }

            _context.Baum.Remove(dbBaum);

            await _context.SaveChangesAsync();
            return Ok(true);
        }

    }
}
