using deinbaumApp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel.Startup
{
    /// <summary>
    /// Viewmodel fuer die Login Seite
    /// </summary>
    public partial class LoginPageViewModel :BaseViewModel
    {
        [ObservableProperty]
        private string loginStr;

        [ObservableProperty]
        private string password;

        private readonly LoginService loginService;
        public LoginPageViewModel(LoginService loginService)
        {
            this.loginService = loginService;
            Title = "Login";
        }

        #region Commands
        /// <summary>
        /// Methode um ein User einzuloggen
        /// Token wird in SecureStorage gespeichert
        /// </summary>
        [RelayCommand]
        async void Login()
        {
            if (!string.IsNullOrWhiteSpace(LoginStr) && !string.IsNullOrWhiteSpace(Password))
            {
                var response = await loginService.Authenticate(new LoginRequest
                {
                    Login = LoginStr,
                    Password = Password
                });

                if (response != null)
                {

                    if (Preferences.ContainsKey(nameof(App.UserDetails)))
                    {
                        Preferences.Remove(nameof(App.UserDetails));
                    }

                    // Token speichern
                    await SecureStorage.Default.SetAsync("oauth_token", response.Token);

                    // Userdetails speichern
                    var userDetails = new UserBasicInfo();
                    userDetails.Login = LoginStr;
                    userDetails.IstAdminBerechtigt = response.IstAdminBerechtigt;
                    string userDetailStr = JsonConvert.SerializeObject(userDetails);
                    Preferences.Set(nameof(App.UserDetails), userDetailStr);
                    App.UserDetails = userDetails;
                    App.Token = response.Token;

                    // FlyoutItems je nach Rolle zusammenstellen
                    await AppConstant.AddFlyoutMenusDetails();
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Login fehlgeschlagen", "Login fehlgeschlagen. Bitte erneut versuchen", "Ok");
                }
            }
        }
        #endregion
    }
}
