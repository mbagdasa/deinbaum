using BruTile.Cache;
using BruTile.Predefined;
using deinbaumApp.Services;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Nts;
using Mapsui.Projections;
using Mapsui.Providers.Wms;
using Mapsui.Styles;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.UI;
using Mapsui.UI.Maui;
using Mapsui.Widgets.Zoom;
using System.Xml;

namespace deinbaumApp.Views.Dashboard;

/// <summary>
/// CodeBehind anstelle Viewmodel, da explizit auf Controlname der karte zugegriffen werden muss
/// </summary>
[QueryProperty("Latitude", "Latitude")]
[QueryProperty("Longitude", "Longitude")]
public partial class MapSuiViewPage : ContentPage
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    MapService mapService;

    public MapSuiViewPage(MapViewModel viewModel, MapService mapService)
	{
		InitializeComponent();
        BindingContext = viewModel;
        this.mapService = mapService;
    }

    /// <summary>
    /// Neue Karte generieren mit neuem Pin der gesetzen Position des Baumes
    /// </summary>
    protected override void OnAppearing()
    {
        base.OnAppearing();

        var mapControl = new Mapsui.UI.Maui.MapControl();

        //Latitude = 47.3238201;
        //Longitude = 8.8001518;

        //Latitude = 47.32269218043851;
        //Longitude = 8.798319736346864;

        mapControl.Map = mapService.CreateMapAsync(Longitude, Latitude).Result;

        var layer = new GenericCollectionLayer<List<IFeature>>
        {
            Style = SymbolStyles.CreatePinStyle()
        };
        // Add a point to the layer using the Info position
        layer?.Features.Add(new GeometryFeature
        {
            Geometry = new NetTopologySuite.Geometries.Point(SphericalMercator.FromLonLat(Longitude, Latitude).ToMPoint().X, SphericalMercator.FromLonLat(Longitude, Latitude).ToMPoint().Y)
        });
        // To notify the map that a redraw is needed.
        layer?.DataHasChanged();

        mapControl.Map.Layers.Add(layer);

        mapView.Content = mapControl;
    }

}