using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.PersonDaten;
using deinbaumApp.Helpers;
using Newtonsoft.Json;

namespace deinbaumApp.Services
{
    /// <summary>
    /// Service um die Baeume via WebAPI zu verwalten (CRUD-Operationen)
    /// </summary>
    public class BaumService: ServiceAbstract
    {
        List<Baum> baumListe = new();
        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public BaumService()
        {
            url = $"{BaseAddress}/api/Baum";
        }

        /// <summary>
        /// Speichern eines neuen Baumes asynchron
        /// </summary>
        /// <param name="baum"></param>
        /// <returns>Task</returns>
        public async Task SaveBaum(Baum baum)
        {
            try
            {
                if (baum is null)
                    return;

                string baumStr = JsonConvert.SerializeObject(baum);

                var response = Client.PostAsync(url,
                             new StringContent(baumStr, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();
;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await AppShell.Current.DisplayAlert("Erfassung Baum", "Baum erfolreich erfasst", "Ok");
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Baum Erfassung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Speichern fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Aktualisieren eines bestehenden Baumes asynchron
        /// </summary>
        /// <param name="baum"></param>
        /// <returns>Task</returns>
        public async Task UpdateBaum(Baum baum)
        {
            try
            {
                if (baum is null)
                    return;

                string baumStr = JsonConvert.SerializeObject(baum);

                var response = Client.PutAsync(url,
                             new StringContent(baumStr, Encoding.UTF8, "application/json")).GetAwaiter().GetResult();

                var result = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await AppShell.Current.DisplayAlert("Aktualisierung Baum", $"Baum '{baum.Name}' erfolreich aktualisiert", "Ok");
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Baum Aktualisierung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Speichern fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        } 

        /// <summary>
        /// Loeschen eines bestehenden Baumes asynchron
        /// </summary>
        /// <param name="baum"></param>
        /// <returns>Task</returns>
        public async Task DeleteBaum(Baum baum)
        {
            try
            {
                if (baum is null)
                    return;

                string baumStr = JsonConvert.SerializeObject(baum);

                var response = Client.DeleteAsync($"{url}?Id={baum.ID}").GetAwaiter().GetResult();

                var result = response.Content.ReadAsStringAsync().Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await AppShell.Current.DisplayAlert("Löschen Baum", "Baum erfolreich gelöscht", "Ok");
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Baum Löschung fehlgeschlagen", $"{resultString}", "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Löschen fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }
        }

        /// <summary>
        /// Bestehende Baeume als Liste asynchron holen
        /// </summary>
        /// <returns>Task<List<Baum>></returns>
        public async Task<List<Baum>> GetBauemeAsync()
        {
            List<Baum> baumListe = new();
            try
            {
                var response = await Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    baumListe = await response.Content.ReadFromJsonAsync<List<Baum>>();
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Zugriff auf Bäume fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
            }

            return baumListe;
        }

        /// <summary>
        /// Alle bestehenden Mitarbeiter als Liste holen
        /// </summary>
        /// <returns>List<Feldmitarbeiter></returns>
        public List<Baum> GetBaueme()
        {
            List<Baum> baumListe = new();
            var response = Client.GetAsync(url).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                baumListe = response.Content.ReadFromJsonAsync<List<Baum>>().Result;
            }
            return baumListe;
        }
    }
}
