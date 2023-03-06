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
    /// Service um die Baummerkmale via WebAPI zu verwalten (CRUD-Operationen)
    /// TODO: Zurzeit nur Read Operation
    /// </summary>
    public class BaumMerkmaleService : ServiceAbstract
    {
        List<BaumMerkmal> baumMerkmale;

        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public BaumMerkmaleService()
        {
            url = $"{BaseAddress}/api/BaumMerkmal";
        }

        /// <summary>
        /// Baummerkmale via WebAPI holen
        /// </summary>
        /// <returns>List<BaumMerkmal></returns>
        public List<BaumMerkmal> GetBaumMerkmale()
        {
            if (baumMerkmale?.Count > 0)
                return baumMerkmale;

            var response = Client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                baumMerkmale = response.Content.ReadFromJsonAsync<List<BaumMerkmal>>().GetAwaiter().GetResult();
            }
            return baumMerkmale;
        }

        /// <summary>
        /// Baummerkmale via WebAPI asynchron holen
        /// </summary>
        /// <returns>Task<List<BaumMerkmal>></returns>
        public async Task<List<BaumMerkmal>> GetBaumMerkmaleAsync()
        {
            if (baumMerkmale?.Count > 0)
                return baumMerkmale;

            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                baumMerkmale = await response.Content.ReadFromJsonAsync<List<BaumMerkmal>>();
            }

            return baumMerkmale;
        }
    }
}
