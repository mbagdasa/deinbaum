using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace deinBaum.Lib.BaumStruktur
{/**
     *  Beschreibung:
     *  Zweck der Klasse:
     *  
     *  Mögliche Arten siehe Json File
     *  
     *  @author Luxson, Micha, Lidja
     *  @version 08.12.2022
     * 
     * */
    public class BaumArt : ObservableObject
    {
        public int ID { get; set; } = 1;
        public string Art { get; set; }

        public BaumArt()
        {

        }
        public BaumArt(string art)
        {
            Art = art;
        }


        public override string ToString()
        {
            return Art.ToString();
        }

    }
}
