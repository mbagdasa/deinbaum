using deinBaum.Lib.FotoStruktur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.Lib.PersonDaten
{
    public class Feldmitarbeiter : IPerson
    {
        public int ID { get; set; }
        public string Name { get; set ; }
        public string Vorname { get; set; }
        //  protected string Passwort { get; set; }
        public byte[]? Profilbild { get; set; }
        public bool ArbeitetNochInDerFirma { get; set; } 
        public string Login { get; set; }

        public Feldmitarbeiter()
        {

        }
        public Feldmitarbeiter(int id, string name, string vorname, string login)
        {
            ID = id;
            Name = name;
            Vorname = vorname;
            // Passwort = passwort;
           
            ArbeitetNochInDerFirma = true;
            Login = login;
        }
        
    }
}
