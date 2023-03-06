using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.PersonDaten;
using deinbaumApp.Services;
using deinbaumApp.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel um alle bestehenden Waldeigentuemer aufzulisten
    /// </summary>
    [QueryProperty("SelectMode", "SelectMode")]
    public partial class WaldeigentuemerViewModel: BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        ObservableCollection<Waldeigentuemer> waldeigentuemerListe = new();

        private ObservableCollection<Waldeigentuemer> waldeigentuemerListeBackup;

        [ObservableProperty]
        string searchText;

        [ObservableProperty]
        bool selectMode = false;

        WaldeigentuemerService waldeigentuemerService;
        IConnectivity connectivity;

        /// <summary>
        /// Konstruktor mit Dependency Injection
        /// </summary>
        /// <param name="waldeigentuemerService"></param>
        /// <param name="connectivity"></param>
        public WaldeigentuemerViewModel(WaldeigentuemerService waldeigentuemerService, IConnectivity connectivity)
        {
            Title = "Waldeigentümer";
            this.waldeigentuemerService = waldeigentuemerService;
            this.connectivity = connectivity;
            _ = GetWaldeigentuemerAsync();
        }

        #region Properties Changed Methods

        /// <summary>
        /// Waldeigentuemerliste neu abfuellen wenn Suchtext aendert
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                WaldeigentuemerListe = new ObservableCollection<Waldeigentuemer>(waldeigentuemerListeBackup);
            }
            else
            {
                WaldeigentuemerListe = new ObservableCollection<Waldeigentuemer>(waldeigentuemerListeBackup
                    .Where(m => ($"{m.Name}{m.Vorname}").ToLower().Contains(value.ToLower()))
                    .ToList<Waldeigentuemer>());
            }
        }

        #endregion

        /// <summary>
        /// Alle Waldeigentuemer als Liste holen
        /// </summary>
        [RelayCommand]
        async Task GetWaldeigentuemerAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var result = await waldeigentuemerService.GetWaldeigentuemerAsync();

                WaldeigentuemerListe = new ObservableCollection<Waldeigentuemer>(result);
                waldeigentuemerListeBackup = new ObservableCollection<Waldeigentuemer>(result);

                waldeigentuemerListeBackup = WaldeigentuemerListe;

                // Search wieder hinzufuegen
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    WaldeigentuemerListe = new ObservableCollection<Waldeigentuemer>(waldeigentuemerListeBackup
                    .Where(m => ($"{m.Name}{m.Vorname}{m.Email}").ToLower().Contains(SearchText.ToLower()))
                    .ToList<Waldeigentuemer>());
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Fehler bei Zugriff auf bestehende Waldeigentümer", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Navigation zur Baum Seite oder zur Bearbeitungsseite eines Waldeigentuemers
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        [RelayCommand]
        async Task GoTo(Waldeigentuemer waldeigentuemer)
        {
            if (waldeigentuemer == null)
                return;

            if (SelectMode)
            {
                await Shell.Current.GoToAsync("..", true, new Dictionary<string, object>
                {
                    {"Waldeigentuemer", waldeigentuemer }
                });
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(WaldeigentuemerRegisterPage), true, new Dictionary<string, object>
                {
                    {"Waldeigentuemer", waldeigentuemer }
                });
            }
            SelectMode = false;
        }
    }
}
