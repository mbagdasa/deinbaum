using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("BaumMerkmal")]
    public class BaumMerkmalDTO
    {
        [Column("BaumMerkmalID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(255)]
        public string Merkmal { get; set; }

        public ICollection<BaumMerkmalRelationDTO> BaumMerkmalRelation { get; set; }

    }
}
