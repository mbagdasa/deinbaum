using deinBaum.Lib.BaumStruktur;
using deinbaumApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Services
{
    /// <summary>
    /// Service um die Baumarten via WebAPI zu verwalten (CRUD-Operationen)
    /// TODO: Zurzeit nur Read Operation
    /// </summary>
    public class BaumArtenService: ServiceAbstract
    {
        List<BaumArt> baumArten;
        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public BaumArtenService()
        {
            url = $"{BaseAddress}/api/BaumArt";
        }

        /// <summary>
        /// Baumarten via WebAPI holen
        /// </summary>
        /// <returns>List<BaumArt></returns>
        public List<BaumArt> GetBaumArten()
        {
            if (baumArten?.Count > 0)
                return baumArten;

            var response = Client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                baumArten = response.Content.ReadFromJsonAsync<List<BaumArt>>().GetAwaiter().GetResult();
            }
            return baumArten;
        }

        /// <summary>
        /// Baumarten via WebAPI asynchron holen
        /// </summary>
        /// <returns>Task<List<BaumArt>></returns>
        public async Task<List<BaumArt>> GetBaumArtenAsync()
        {
            if (baumArten?.Count > 0)
                return baumArten;

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                baumArten = await response.Content.ReadFromJsonAsync<List<BaumArt>>();
            }
            return baumArten;
        }
    }
}
