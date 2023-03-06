using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.FotoStruktur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("Baum")]
    public class BaumDTO
    {
        [Column("BaumID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        public int ParzellenNr { get; set; }

        [MaxLength(255)]
        public string? Name { get; set; }

        [Required]
        public DateTime ErsteErfassung { get; set; }

        [Required]
        public DateTime LetzteBearbeitung { get; set; }

        [Required]
        public double Baumhoehe { get; set; }

        [Required]
        public double Umfang { get; set; }

        public int? Alter { get; set; }

        public ICollection<BaumZustandRelationDTO> BaumZustandRelation { get; set; }

        public ICollection<BaumMerkmalRelationDTO> BaumMerkmalRelation { get; set; }

        public int? ArtID { get; set; }
        public BaumArtDTO? Art { get; set; }

        public ICollection<FotoDTO>? FotoListe { get; set; }

        public string? Bemerkung { get; set; }

        [Required]
        public double WGS84_XKoordinaten { get; set; }
        [Required]
        public double WGS84_YKoordinaten { get; set; }

        public double? EllipsoidischeHoehe { get; set; }

        public double? HoeheMeterUeberMeer { get; set; }

        [Required]
        public double LV95_EKoordinaten { get; set; }
        [Required]
        public double LV95_NKoordinaten { get; set; }

        // Darf nicht Required und muss nullable sein, da sonst Cascade Constraints angelegt werden!
        public int? FeldmitarbeiterID { get; set; }
        public FeldmitarbeiterDTO? Feldmitarbeiter { get; set; }

        // Darf nicht Required und muss nullable sein, da sonst Cascade Constraints angelegt werden!
        public int? WaldeigentuemerID { get; set; }
        public WaldeigentuemerDTO? Waldeigentuemer { get; set; }
    }
}
