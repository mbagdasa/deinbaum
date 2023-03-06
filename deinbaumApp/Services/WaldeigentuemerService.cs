using deinBaum.Lib.PersonDaten;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Services
{
    /// <summary>
    /// Service um Waldeigentuemer via WebAPI zu verwalten (CRUD-Operationen)
    /// TODO: Noch kein Delete von Waldeigentuemer
    /// Es koennen bestehende Daten auf geloeschte Waldeigentuemer zeigen.
    /// </summary>
    public class WaldeigentuemerService : ServiceAbstract
    {
        List<Waldeigentuemer> waldEigentuemerList;
        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public WaldeigentuemerService()
        {
            url = $"{BaseAddress}/api/Waldeigentuemer";
        }

        /// <summary>
        /// Speichern eines neuen Waldeigentuemers
        /// </summary>
        /// <param name="waldeigentuemer"></param>
        /// <returns>Task</returns>
        public async Task Register(Waldeigentuemer waldeigentuemer)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(waldeigentuemer);

                var response = Client.PostAsync(url, new StringContent(requestString, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await AppShell.Current.DisplayAlert("Erfassung Waldeigentümer", "Waldeigentümer erfolreich erfasst", "Ok");
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Waldeigentümer Erfassung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Speichern fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Aktualisieren eines bestehenden Waldeigentuemers
        /// </summary>
        /// <param name="waldeigentuemer"></param>
        /// <returns>Task</returns>
        public async Task Update(Waldeigentuemer waldeigentuemer)
        {
            try
            {
                string requestString = JsonConvert.SerializeObject(waldeigentuemer);

                var response = await Client.PutAsync($"{url}/updateWaldeigentuemer/id/{waldeigentuemer.ID}",
                            new StringContent(requestString, Encoding.UTF8, "application/json"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await AppShell.Current.DisplayAlert("Anpassung Waldeigentümer", "Waldeigentümer erfolreich angepasst", "Ok");
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Waldeigentümer Aktualisierung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Speichern fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Bestehende Waldeigentuemer in Liste asynchron holen
        /// </summary>
        /// <returns>Task<List<Waldeigentuemer>></returns>
        public async Task<List<Waldeigentuemer>> GetWaldeigentuemerAsync()
        {
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                waldEigentuemerList = await response.Content.ReadFromJsonAsync<List<Waldeigentuemer>>();
            }
            return waldEigentuemerList;
        }
    }
}
