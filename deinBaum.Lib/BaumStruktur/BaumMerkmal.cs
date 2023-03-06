using CommunityToolkit.Mvvm.ComponentModel;
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
    public class BaumMerkmal : ObservableObject
    {
        public int ID { get; set; } = 1;

        public string Merkmal { get; set; }

        public BaumMerkmal(string merkmal)
        {
            Merkmal = merkmal;
        }

        public override string ToString()
        {
            return Merkmal.ToString();
        }
    }
}
