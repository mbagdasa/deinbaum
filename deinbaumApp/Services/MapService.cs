using BruTile.Cache;
using BruTile.Predefined;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Providers.Wms;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.UI.Maui;
using Mapsui.Widgets.Zoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapsui.Styles;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.UI;


namespace deinbaumApp.Services
{
    /// <summary>
    /// Service um Karte aufzubereiten mittels MapSui
    /// </summary>
    public class MapService
    {
        private IPersistentCache<byte[]>? defaultCache = null;

        /// <summary>
        /// Erstellen einer Karte mit Bing Orthofoto Hintergrund
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public Task<Mapsui.Map> CreateMapAsync(double lon, double lat)
        {
            return Task.FromResult(CreateMap(lon, lat, null));
        }

        public Mapsui.Map CreateMap(double lon, double lat, IPersistentCache<byte[]>? persistentCache, KnownTileSource source = KnownTileSource.BingAerial)
        {
            var map = new Mapsui.Map();

            var apiKey = "AlJt9AAqlphoKNJCvqgqIzKrOzejvTNuHBOQkpAsZwsbzUhYdgEe4mLHtBOByiMI"; // Contact Microsoft about how to use this
            map.Layers.Add(new TileLayer(KnownTileSources.Create(source, apiKey, persistentCache),
                dataFetchStrategy: new DataFetchStrategy()) // DataFetchStrategy prefetches tiles from higher levels
            {
                Name = "Bing Aerial",
            });

            var point = SphericalMercator.FromLonLat(lon, lat).ToMPoint();


            //map.Home = n => n.NavigateTo(new MPoint(970880.698384544, 5993063.77875121), map.Resolutions[18]);
            map.Home = n => n.NavigateTo(point, map.Resolutions[14]);
            map.BackColor = Mapsui.Styles.Color.FromString("#000613");

            return map;
        }

        /// <summary>
        /// Methode um Info der Karte bei einem Klick zu verarbeiten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapOnInfo(object? sender, MapInfoEventArgs e)
        {
            var calloutStyle = e.MapInfo?.Feature?.Styles.Where(s => s is CalloutStyle).Cast<CalloutStyle>().FirstOrDefault();
            if (calloutStyle != null)
            {
                calloutStyle.Enabled = !calloutStyle.Enabled;
                e.MapInfo?.Layer?.DataHasChanged(); // To trigger a refresh of graphics.
            }
        }
    }
}
