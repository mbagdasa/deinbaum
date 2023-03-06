using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinBaum.Lib.FotoStruktur
{
    /**
     * Beschreibung: TODO
     * Zweck der Klasse: TODO
     * 
     * @author Micha, Luxson, Lidja
     * @version 08.12.2022
     */
    public class Foto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public byte[] Fotobytes { get; set; }

        public Foto()
        {

        }

        public Foto(string name, byte[] fotobytes)
        {
            Name = name;
            Fotobytes = fotobytes;
        }
    }
}
