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
    /// Viewmodel um alle erfassten Mitarbeiter aufzulisten
    /// SelectMode sagt aus, ob ein Mitarbeiter fuer einen Baum ausgewaehlt wird oder der Mitarbeiter bearbeitet werden soll
    /// </summary>
    [QueryProperty("SelectMode", "SelectMode")]
    public partial class MitarbeiterViewModel : BaseViewModel
    {

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        ObservableCollection<Feldmitarbeiter> mitarbeiterListe = new();

        private ObservableCollection<Feldmitarbeiter> mitarbeiterListeBackup;

        [ObservableProperty]
        string searchText;

        [ObservableProperty]
        bool selectMode = false;

        UserService userService;
        IConnectivity connectivity;
        
        /// <summary>
        /// Konstruktor mit Dependency Injection
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="connectivity"></param>
        public MitarbeiterViewModel(UserService userService, IConnectivity connectivity)
        {
            Title = "Mitarbeiter";
            this.userService = userService;
            this.connectivity = connectivity;
            GetMitarbeiter();
        }

        #region Properties Changed Methods

        /// <summary>
        /// Mitarbeiterliste neu abfuellen wenn Suchtext aendert
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                MitarbeiterListe = new ObservableCollection<Feldmitarbeiter>(mitarbeiterListeBackup);
            }
            else
            {
                MitarbeiterListe = new ObservableCollection<Feldmitarbeiter>(mitarbeiterListeBackup
                    .Where(m => ($"{m.Name}{m.Vorname}").ToLower().Contains(value.ToLower()))
                    .ToList<Feldmitarbeiter>());
            }
        }

        /// <summary>
        /// Ausfiltern von Mitarbeitern, welche nicht mehr in der Firma arbeiten,
        /// wenn ein Mitarbeiter einem Baum zugewiesen werden muss
        /// </summary>
        /// <param name="value"></param>
        partial void OnSelectModeChanged(bool value)
        {
            if (value)
            {
                mitarbeiterListeBackup = new ObservableCollection<Feldmitarbeiter>(mitarbeiterListeBackup
                   .Where(m => m.ArbeitetNochInDerFirma == true)
                   .ToList<Feldmitarbeiter>());
                MitarbeiterListe = mitarbeiterListeBackup;
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Alle Mitarbeiter als Liste holen
        /// </summary>
        [RelayCommand]
        void GetMitarbeiter()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var result = userService.GetMitarbeiter();

                MitarbeiterListe = new ObservableCollection<Feldmitarbeiter>(result);
                mitarbeiterListeBackup = new ObservableCollection<Feldmitarbeiter>(result);

                // Search wieder hinzufuegen
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    MitarbeiterListe = new ObservableCollection<Feldmitarbeiter>(mitarbeiterListeBackup
                    .Where(m => ($"{m.Name}{m.Vorname}").ToLower().Contains(SearchText.ToLower()))
                    .ToList<Feldmitarbeiter>());
                }

            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Fehler", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Navigation zur Baum Seite oder zur Bearbeitungsseite eines Mitarbeiters
        /// </summary>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        [RelayCommand]
        async Task GoTo(Feldmitarbeiter mitarbeiter)
        {
            if (mitarbeiter == null)
                return;

            var navUri = Shell.Current.CurrentState.Location.ToString();

            if (SelectMode)
            {
                await Shell.Current.GoToAsync($"..", true, new Dictionary<string, object>
                {
                    {"Feldmitarbeiter", mitarbeiter }
                });
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(MitarbeiterRegisterPage), true, new Dictionary<string, object>
                {
                    {"Feldmitarbeiter", mitarbeiter }
                });
            }
            SelectMode = false;
        }

        #endregion

    }
}
