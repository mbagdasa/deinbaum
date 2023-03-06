using deinBaum.Lib.BaumStruktur;
using deinbaumApp.Services;
using Mapsui;
using Mapsui.UI;
using Mapsui.UI.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel fuer Karte
    /// TODO: Leider kann nur explizit auf das Kartencontrol mit dem Namen zugegriffen werden
    /// Deshalb ist die Logik im CodeBehind File
    /// </summary>
    [QueryProperty("Latitude", "Latitude")]
    [QueryProperty("Longitude", "Longitude")]
    public partial class MapViewModel : BaseViewModel
    {
        MapService mapService;

        [ObservableProperty]
        double latitude;
        [ObservableProperty]
        double longitude;

        [ObservableProperty]
        IMapControl karte;

        public IMapControl CurrentMapView { get; set; }        


        public MapViewModel(MapService mapService)
        {
            this.mapService = mapService;
        }
    }
}
