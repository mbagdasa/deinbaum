using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata;

namespace deinBaum.DAL.Model
{
    [Table("Foto")]
    public class FotoDTO
    {
        [Column("FotoID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public byte[] Fotobytes { get; set; }

        public int BaumID { get; set; }
        public BaumDTO Baum { get; set; }
    }
}
