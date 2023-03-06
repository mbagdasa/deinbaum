using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("Waldeigentuemer")]
    public class WaldeigentuemerDTO
    {
        [Column("WaldeigentuemerID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } 

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required, MaxLength(150)]
        public string Vorname { get; set; }

        [Required]
        public int PLZ { get; set; }

        [Required, MaxLength(200)]
        public string Ort { get; set; }

        public string? MobileNr { get; set; } // Eindeutiges Format ? +41 77 777 77 77

        [Required]
        public string Email { get; set; }
    }
}
