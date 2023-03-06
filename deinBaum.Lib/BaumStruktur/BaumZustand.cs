using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.Lib.BaumStruktur
{
    /**
     *  Beschreibung:
     *  Zweck der Klasse:
     *  
     *  Mögliche Zustände, lebendig, tot, stehend, liegend
     *  
     *  @author Luxson, Micha, Lidja
     *  @version 08.12.2022
     * 
     * */
    public class BaumZustand
    {
        public int ID { get; set; }

        public string Zustand { get; set; }

        public bool IsChecked { get; set; }

        public BaumZustand(string zustand)
        {
            Zustand = zustand;
        }

        public override string ToString()
        {
            return Zustand.ToString();
        }
    }
}
