using deinBaum.DAL.Model;
using deinBaum.Lib.PersonDaten;
using deinBaum.WebAPI.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LoginResponse = deinBaum.Lib.PersonDaten.LoginResponse;

namespace deinBaum.WebAPI.Test
{
    [TestFixture]
    public class FeldmitarbeiterControllerTest
    {
        public HttpClient HttpClient { get; set; }
        public Uri BaseAdress { get; set; }
        public User User { get; set; }


        [SetUp]
        public void Setup()
        {
            BaseAdress = new Uri("https://localhost:7001/api/");
            HttpClient = new HttpClient();
            User = new()
            {
                Login = $"testAdmin",
                Password = "test123"
            };
        }


        /// <summary>
        /// Registriert ein Feldmitarbeiter gibt ein True zurück und löscht Sie wieder
        /// </summary>
        /// <returns></returns>
        [Test, Order(1)]
        public async Task RegisterFeldmitarbeiter()
        {
            FeldmitarbeiterDTO dto = new FeldmitarbeiterDTO()
            {
                Name = "Tester",
                Vorname = "Admin",
                Profilbild = null,
                ArbeitetNochInDerFirma = true,
                Login = "testAdmin"
            };
            try
            {
                string request = JsonConvert.SerializeObject(User);

                var response = await HttpClient.PostAsync(BaseAdress + "Auth/register/",
                             new StringContent(request, Encoding.UTF8, "application/json"));

                response = await HttpClient.PostAsync(BaseAdress + "Auth/login/",
                             new StringContent(request, Encoding.UTF8, "application/json"));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);

                    HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", loginResponse.Token);

                   
                    //Registrierung Feldmitarbeiter
                    request = JsonConvert.SerializeObject(dto);
                    response = await HttpClient.PostAsync($"{BaseAdress.AbsoluteUri}Feldmitarbeiter/",
                            new StringContent(request, Encoding.UTF8, "application/json"));
                   
                    Assert.IsTrue(response.IsSuccessStatusCode);
                }

            }
            finally
            {

                    var isDeletedResponse  = await HttpClient.DeleteAsync(BaseAdress.AbsoluteUri + "Feldmitarbeiter" + "?login=" + dto.Login);

                    Assert.IsTrue(isDeletedResponse.IsSuccessStatusCode);

                     isDeletedResponse = await HttpClient.DeleteAsync("https://localhost:7001/api/Auth?login=" + User.Login);

                    Assert.IsTrue(isDeletedResponse.IsSuccessStatusCode);
                
            }
           
        }

        
    }
}
