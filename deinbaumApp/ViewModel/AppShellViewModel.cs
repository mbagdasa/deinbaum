using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using deinbaumApp.Views.Startup;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel fuer dieFlyout Navigations-Seite
    /// </summary>
    public partial class AppShellViewModel : BaseViewModel
    {
        /// <summary>
        /// Logout um wieder auf die Login Seite zu kommen
        /// </summary>
        [RelayCommand]
        async void Logout()
        {
            if (Preferences.ContainsKey(nameof(App.UserDetails)))
            {
                Preferences.Remove(nameof(App.UserDetails));
            }
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
