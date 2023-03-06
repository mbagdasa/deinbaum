using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using deinBaum.Lib.FotoStruktur;
using deinBaum.Lib.PersonDaten;

namespace deinBaum.Lib.BaumStruktur
{
    /**
     * Beschreibung: TODO
     * Zweck der Klasse: ViewModel
     * 
     * @author Micha, Luxson, Lidja
     * @version 08.12.2022
     */
    public partial class Baum : ObservableObject
    {      
        public int ID { get; set; }
        public int? ParzellenNr { get; set; }

        public string? Name { get; set; }

        public DateTime ErsteErfassung { get; set; }
        public DateTime LetzteBearbeitung { get; set; }

        public double? Baumhoehe { get; set; }

        public double? Umfang { get; set; }

        public int? Alter { get; set; }

        public List<BaumZustand>? ZustandsListe { get; set; }

        public List<BaumMerkmal>? Merkmale { get; set; }

        public BaumArt? Art { get; set; }

        public List<Foto> FotoListe { get; set; }

        public string? Bemerkung { get; set; }


        public double WGS84_XKoordinaten { get; set; }

        public double WGS84_YKoordinaten { get; set; }

        public double? EllipsoidischeHoehe { get; set; }

        public double? HoeheMeterUeberMeer { get; set; }

        public double LV95_EKoordinaten { get; set; }
        public double LV95_NKoordinaten { get; set; }

        public Feldmitarbeiter? Feldmitarbeiter { get; set; }

        public Waldeigentuemer? Waldeigentuemer { get; set; }
    }
}
