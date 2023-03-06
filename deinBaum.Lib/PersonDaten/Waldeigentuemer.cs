using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.Lib.PersonDaten
{
    public class Waldeigentuemer : IPerson
    {
        public int ID { get; set; }
        public string Name { get ; set ; }
        public string Vorname { get ; set ; }
        public int PLZ { get ; set ; }
        public string Ort { get; set; }
        public string? MobileNr { get; set; }
        public string Email { get; set; }

        public Waldeigentuemer()
        {
        }

        public Waldeigentuemer(string name, string vorname, int plz, string ort, string email)
        {
            Name = name;
            Vorname = vorname;
            PLZ = plz;
            Ort = ort;
            Email = email;
        }
    }
}
