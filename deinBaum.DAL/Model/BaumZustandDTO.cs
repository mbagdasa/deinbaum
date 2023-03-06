using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("BaumZustand")]
    public class BaumZustandDTO
    {
        [Column("BaumZustandID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Zustand { get; set; }

        public ICollection<BaumZustandRelationDTO> BaumZustandRelation { get; set; }
    }
}
