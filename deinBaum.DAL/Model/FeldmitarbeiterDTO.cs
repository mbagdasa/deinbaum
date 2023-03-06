using deinBaum.Lib.FotoStruktur;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("Feldmitarbeiter")]
    public class FeldmitarbeiterDTO
    {
        [Column("FeldmitarbeiterID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Vorname { get; set; }

        public byte[]? Profilbild { get; set; }

        [Required]
        public bool ArbeitetNochInDerFirma { get; set; }

        [Required]
        [ForeignKey("Loginname")]
        public string Login { get; set; }
    }
}
