using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.PersonDaten;
using deinbaumApp.Helpers;
using ExCSS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Services
{
    /// <summary>
    /// Service um Mitarbeiter/User via WebAPI zu verwalten (CRUD-Operationen)
    /// TODO: Noch kein Delete von Feldmitarbeiter
    /// Es koennen bestehende Daten auf geloeschte Feldmitarbeiter zeigen.
    /// Deshalb nur Flag 'ArbeitetNochInFirma'
    /// </summary>
    public class UserService: ServiceAbstract
    {

        List<Feldmitarbeiter> feldMitarbeiterList;
        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public UserService()
        {
            url = $"{BaseAddress}/api/Feldmitarbeiter";
        }

        /// <summary>
        /// Speichern eines Feldmitarbeiters
        /// Es wird ebenfalls auch ein User für das Login angelegt
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <param name="user"></param>
        /// <returns>Task</returns>
        public async Task Register(Feldmitarbeiter mitarbeiter, User user)
        {
            try
            {

                string requestString = JsonConvert.SerializeObject(mitarbeiter);

                // 1.) Feldmitarbeiter anlegen
                var responseFeldmitarbeiter = await Client.PostAsync(url,
                                new StringContent(requestString, Encoding.UTF8, "application/json")
                                );

                if (responseFeldmitarbeiter.StatusCode == HttpStatusCode.OK)
                {
                    // 2.) User anlegen
                    requestString = JsonConvert.SerializeObject(user);
                    var response = Client.PostAsync($"{BaseAddress}/api/Auth/register",
                                new StringContent(requestString, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        await AppShell.Current.DisplayAlert("Erfassung Feldmitarbeiter", $"Feldmitarbeiter '{mitarbeiter.Login}' erfolreich erfasst", "Ok");
                    }
                    else
                    {
                        var resultString = responseFeldmitarbeiter.Content.ReadAsStringAsync().Result;
                        await AppShell.Current.DisplayAlert("Feldmitarbeiter Erfassung fehlgeschlagen", $"{resultString}", "Ok");
                    }
                }
                else
                {
                    var resultString = responseFeldmitarbeiter.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Feldmitarbeiter Erfassung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Speichern fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Aktualisieren eines bestehenden Mitarbeiters
        /// TODO: Zurzeit lassen sich die Login Informationen noch nicht aktualisieren
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns>Task</returns>
        public async Task Update(Feldmitarbeiter mitarbeiter)
        {
            try
            {
                if (mitarbeiter is null)
                    return;

                string requestString = JsonConvert.SerializeObject(mitarbeiter);

                var response = await Client.PutAsync($"{url}/updateFeldmitarbeiter/id/{mitarbeiter.ID}",
                            new StringContent(requestString, Encoding.UTF8, "application/json"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await AppShell.Current.DisplayAlert("Aktualisierung Feldmitarbeiter", $"Feldmitarbeiter '{mitarbeiter.Login}' erfolreich aktualisiert", "Ok");
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Feldmitarbeiter Aktualisierung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Speichern fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Alle bestehenden Mitarbeiter als Liste asynchron holen
        /// </summary>
        /// <returns>Task<List<Feldmitarbeiter>></returns>
        public async Task<List<Feldmitarbeiter>> GetMitarbeiterAsync()
        {
            var response = await Client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                feldMitarbeiterList = await response.Content.ReadFromJsonAsync<List<Feldmitarbeiter>>();
            }
            return feldMitarbeiterList;
        }

        /// <summary>
        /// Alle bestehenden Mitarbeiter als Liste holen
        /// </summary>
        /// <returns>List<Feldmitarbeiter></returns>
        public List<Feldmitarbeiter> GetMitarbeiter()
        {
            var response = Client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                feldMitarbeiterList = response.Content.ReadFromJsonAsync<List<Feldmitarbeiter>>().Result;
            }
            return feldMitarbeiterList;
        }
    }
}
