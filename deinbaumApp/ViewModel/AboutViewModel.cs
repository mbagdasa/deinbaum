using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel fuer die Info-Seite
    /// </summary>
    public partial class AboutViewModel : BaseViewModel
    {
        [ObservableProperty]
        string appName;

        [ObservableProperty]
        string version;

        [ObservableProperty]
        string moreInfoUrl;

        [ObservableProperty]
        string message;

        public AboutViewModel()
        {
            Title = "Über";
            AppName = AppInfo.Name;
            Version = AppInfo.VersionString;
            MoreInfoUrl = "https://www.deinbaum.ch/";
            Message = "Diese App wurde mittels XAML und C# .NET MAUI geschrieben. Weitere Infos können hier ergänzt werden...";
        }

        #region Commands
        [RelayCommand]
        async void MoreInfo()
        {
            // Navigate to the specified URL in the system browser.
            await Launcher.Default.OpenAsync(MoreInfoUrl);
        }
        #endregion
    }
}
