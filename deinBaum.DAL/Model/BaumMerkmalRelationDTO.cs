using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    [Table("BaumMerkmalRelation")]
    public class BaumMerkmalRelationDTO
    {
        [ForeignKey("Baum")]
        public int BaumID { get; set; }

        public BaumDTO Baum { get; set; }

        [ForeignKey("BaumMerkmal")]
        public int MerkmalID { get; set; }

        public BaumMerkmalDTO Merkmal { get; set; }

    }
}
