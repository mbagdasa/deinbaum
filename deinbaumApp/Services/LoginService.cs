using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using deinbaumApp.Helpers;

namespace deinbaumApp.Services
{
    /// <summary>
    /// Service fuer das Login
    /// </summary>
    public class LoginService : ServiceAbstract
    {
        string url = String.Empty;

        /// <summary>
        /// Konstruktor, URL wird initialisiert
        /// </summary>
        public LoginService()
        {
            url = $"{BaseAddress}/api/Auth/login";
        }

        /// <summary>
        /// Login mit Loginangaben via WebApi
        /// Falls erfolgreich, kommt ein Token als Antwort
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>Task<LoginResponse></returns>
        public async Task<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            try
            {
                string loginRequestStr = JsonConvert.SerializeObject(loginRequest);

                var response = await Client.PostAsync(url, new StringContent(loginRequestStr, Encoding.UTF8,"application/json"));

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<LoginResponse>(json);
                }
                else
                {
                    var resultString = response.Content.ReadAsStringAsync().Result;
                    await AppShell.Current.DisplayAlert("Login fehlgeschlagen", $"{resultString}", "Ok");
                    return null;
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Login fehlgeschlagen", $"{FailureMsg}: {ex.Message}", "Ok");
                return null;
            }
        }
    }
}
