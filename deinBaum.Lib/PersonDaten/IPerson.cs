using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.Lib.PersonDaten
{
    /**
     * Beschreibung: TODO
     * Zweck der Klasse: TODO
     * 
     * @author Luxson, Micha, Lidja
     * @version 08.12.2022
     * 
     * */
    public interface IPerson
    {
        // TODO Interface Lös
       
        // Nur Feldmitarbeiter
        //public string Kuerzel { get; set; }

         string Name { get; set; }
         string Vorname { get; set; }
        //public string Email { get; set; }
        //public string MobileNr { get; set; }
        //public string PLZ { get; set; }
        //public string Ort { get; set; }

    }
}
