using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("BaumZustandRelation")]
    public class BaumZustandRelationDTO
    {
        [ForeignKey("Baum")]
        public int BaumID { get; set; }

        public BaumDTO Baum { get; set; }

        [ForeignKey("BaumZustand")]
        public int ZustandID { get; set; }

        public BaumZustandDTO Zustand { get; set; }
    }
}
