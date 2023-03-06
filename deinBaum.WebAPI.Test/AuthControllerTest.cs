using deinBaum.DAL.Model;
using deinBaum.Lib.PersonDaten;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Net;
using System.Text;

namespace deinBaum.WebAPI.Test
{
    [TestFixture]
    public class AuthControllerTest
    {
        public HttpClient HttpClient { get; set; }
        public Uri BaseAdress { get; set; }
        public User User { get; set; }

        [SetUp]
        public void Setup()
        {
            BaseAdress = new Uri("https://localhost:7001/api/Auth/");
            HttpClient = new HttpClient();

        }

        /// <summary>
        /// Testet die Speicherung der Datensätze in die DB über einen Rest API Call
        /// Erwarteter Ergebnis True = 200 StatusCode
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(1)]
        public async Task RegisterUserWhichNotExistsInDB()
        {
            User = new()
            {
                Login = $"testAdmin",
                Password = "test123"
            };
            try
            {
                string request = JsonConvert.SerializeObject(User);

                // POST Registrierung
                var response = await HttpClient.PostAsync(BaseAdress + "register/",
                             new StringContent(request, Encoding.UTF8, "application/json"));

                var json = await response.Content.ReadAsStringAsync();
                UserDTO userDto = JsonConvert.DeserializeObject<UserDTO>(json);

                Assert.IsTrue(userDto.IstAdminBerechtigt == true, "Testing erfolgreich!");
            }
            finally
            {
                string request = JsonConvert.SerializeObject(User);

                //Login
                var response = await HttpClient.PostAsync(BaseAdress + "login/",
                             new StringContent(request, Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);

                    HttpClient _client = new HttpClient();
                    _client.BaseAddress = new Uri(BaseAdress.AbsoluteUri);
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", loginResponse.Token);

                    // User löschen
                    var isDeletedResponse = await _client.DeleteAsync("https://localhost:7001/api/Auth?login=" + User.Login);
                    Assert.IsTrue(isDeletedResponse.IsSuccessStatusCode);

                    var result = await isDeletedResponse.Content.ReadAsStringAsync();
                    Assert.IsTrue(bool.Parse(result));
                }
            }


        }

        /// <summary>
        /// Testet die Badrequest wenn man ein bereits existierender User nochmals registriert wird
        /// </summary>
        /// <returns></returns>
        [Test]
        [Order(2)]
        public async Task RegisterUserWhichAlreadyExistsInDB()
        {
            User = new()
            {
                Login = $"testAdmin1",
                Password = "test123"
            };
            try
            {
                string request = JsonConvert.SerializeObject(User);

                // erste Registrierung
                var response = await HttpClient.PostAsync(BaseAdress + "register/",
                             new StringContent(request, Encoding.UTF8, "application/json"));

                // Registerierung nochmals wiederholen
                response = await HttpClient.PostAsync(BaseAdress + "register/",
                             new StringContent(request, Encoding.UTF8, "application/json"));

                Assert.IsFalse(response.IsSuccessStatusCode);
            }
            finally
            {
                string request = JsonConvert.SerializeObject(User);

                var response = await HttpClient.PostAsync(BaseAdress + "login/",
                             new StringContent(request, Encoding.UTF8, "application/json"));
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);

                    HttpClient _client = new HttpClient();
                    _client.BaseAddress = new Uri(BaseAdress.AbsoluteUri);
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", loginResponse.Token);

                    var isDeletedResponse = await _client.DeleteAsync("https://localhost:7001/api/Auth?login=" + User.Login);

                    Assert.IsTrue(isDeletedResponse.IsSuccessStatusCode);

                    var result = await isDeletedResponse.Content.ReadAsStringAsync();
                    Assert.IsTrue(bool.Parse(result));
                }
            }
        }

        


    }
}