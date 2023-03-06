using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.DAL.Model
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ErstelltAm { get; set; } = DateTime.Now;
        public DateTime LaeuftAbAm { get; set; }
    }
}
