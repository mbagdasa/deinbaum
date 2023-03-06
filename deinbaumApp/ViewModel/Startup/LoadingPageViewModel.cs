using deinbaumApp.Views.Startup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel.Startup
{
    /// <summary>
    /// Viewmodel um Login des Users zu ueberpruefen
    /// Falls gespeichertes Token noch gueltig, wird der User
    /// automatisch weitergeschleift
    /// </summary>
    public class LoadingPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public LoadingPageViewModel()
        {
            Title = "Anmeldung...";
            CheckUserLoginDetails();
        }

        /// <summary>
        /// Methode um User Login Details zu ueberpruefen
        /// </summary>
        private async void CheckUserLoginDetails()
        {
            string userDetailsStr = Preferences.Get(nameof(App.UserDetails), "");

            if (string.IsNullOrWhiteSpace(userDetailsStr))
            {
                // navigate to Login Page
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    AppShell.Current.Dispatcher.Dispatch(async () =>
                    {
                        await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    });
                }
                else
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
            }
            else
            {
                var tokenDetail = await SecureStorage.GetAsync("oauth_token");

                if (string.IsNullOrEmpty(tokenDetail))
                {
                    await Shell.Current.DisplayAlert("Benutzersession ist abgelaufen", "Bitte erneut einloggen", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                    return;
                }

                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(tokenDetail) as JwtSecurityToken;


                if (jsonToken.ValidTo < DateTime.UtcNow)
                {
                    await Shell.Current.DisplayAlert("Benutzersession ist abgelaufen", "Bitte erneut einloggen", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }
                else
                {
                    var userInfo = JsonConvert.DeserializeObject<UserBasicInfo>(userDetailsStr);
                    App.UserDetails = userInfo;
                    App.Token = tokenDetail;
                    await AppConstant.AddFlyoutMenusDetails();
                }
            }
        }
    }
}
