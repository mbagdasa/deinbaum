using deinBaum.Lib.BaumStruktur;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Services
{
    /// <summary>
    /// Service um die Baumzustaende via WebAPI zu verwalten (CRUD-Operationen)
    /// TODO: Zurzeit nur Read Operation
    /// </summary>
    public class BaumZustandService: ServiceAbstract
    {
        List<BaumZustand> baumZustaende;

        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public BaumZustandService()
        {
            url = $"{BaseAddress}/api/BaumZustand";
        }

        /// <summary>
        /// Baumzustaende via WebAPI holen
        /// </summary>
        /// <returns>List<BaumMerkmal></returns>
        public List<BaumZustand> GetBaumZustaende()
        {
            if (baumZustaende?.Count > 0)
                return baumZustaende;

            var response = Client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                baumZustaende = response.Content.ReadFromJsonAsync<List<BaumZustand>>().GetAwaiter().GetResult();
            }
            return baumZustaende;
        }
    }
}
