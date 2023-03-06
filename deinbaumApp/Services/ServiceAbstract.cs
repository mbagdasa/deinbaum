using RTools_NTS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.Services
{
    public abstract class ServiceAbstract
    {
        public int Port { get => 7001; }
        public string BaseAddress { get; set; }
        public string FailureMsg { get => "Es ist ein schwerwiegender Fehler aufgetretten. Bitte App schliessen und erneut versuchen"; }

        public HttpClient Client 
        { 
            get 
            {
#if DEBUG
                var devSslHelper = new Helpers.DevHttpsConnectionHelper(sslPort: Port);
                var client = devSslHelper.HttpClient;
#else
                var client = new HttpClient();
#endif
                var token = string.Empty;
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    token = App.Token;
                }
                else
                {
                    token = SecureStorage.Default.GetAsync("oauth_token").Result;
                }

                client.DefaultRequestHeaders.Authorization
                                 = new AuthenticationHeaderValue("Bearer", token);
                client.Timeout = TimeSpan.FromSeconds(20);
                return client;
            }
        }

        public ServiceAbstract()
        {
            BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? $"https://10.0.2.2:{Port}" : $"https://localhost:{Port}";
        }
    }
}
