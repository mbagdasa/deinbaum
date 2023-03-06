using deinBaum.Lib.BaumStruktur;
using deinbaumApp.Services;
using deinbaumApp.Views.Dashboard;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel um die erfassten Baeume darzustellen oder Baeume zu loeschen
    /// </summary>
    public partial class BaeumeAllViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        static ObservableCollection<Baum> baumListe = new();

        private ObservableCollection<Baum> baumListeBackup;

        [ObservableProperty]
        string searchText;

        BaumService baumService;

        /// <summary>
        /// Konstruktor
        /// Services werden als Dependency Injection uebergeben
        /// </summary>
        /// <param name="baumService"></param>
        public BaeumeAllViewModel(BaumService baumService)
        {
            Title = "Alle erfassten Bäume";
            this.baumService = baumService;
            GetBaeume();
        }

        #region Properties Changed Methods

        /// <summary>
        /// Baumliste wird aktualisiert, sobald der Suchtext aendert
        /// </summary>
        /// <param name="value"></param>
        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                BaumListe = new ObservableCollection<Baum>(baumListeBackup);
            }
            else
            {
                BaumListe = new ObservableCollection<Baum>(baumListeBackup
                    .Where(baum => baum.Name.ToLower().Contains(value.ToLower()))
                    .ToList<Baum>());
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Alle erfassten Baeume asynchron holen
        /// </summary>
        /// <returns></returns>
        async Task GetBaeumeAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var result = await baumService.GetBauemeAsync();

                BaumListe = new ObservableCollection<Baum>(result);
                baumListeBackup = new ObservableCollection<Baum>(result);

                // Search wieder hinzufuegen
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    BaumListe = new ObservableCollection<Baum>(BaumListe
                    .Where(baum => baum.Name.ToLower().Contains(SearchText.ToLower()))
                    .ToList<Baum>());
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Fehler bei Zugriff auf bestehende Bäume", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Alle erfassten Baeume asynchron holen
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        void GetBaeume()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var result = baumService.GetBaueme();

                BaumListe = new ObservableCollection<Baum>(result);
                baumListeBackup = new ObservableCollection<Baum>(result);

                // Search wieder hinzufuegen
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    BaumListe = new ObservableCollection<Baum>(BaumListe
                    .Where(baum => baum.Name.ToLower().Contains(SearchText.ToLower()))
                    .ToList<Baum>());
                }

            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Fehler bei Zugriff auf bestehende Bäume", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Weiterleitung auf die Baum-Detail Seite,
        /// um den Baum zu bearbeiten
        /// </summary>
        /// <param name="baum"></param>
        /// <returns></returns>
        [RelayCommand]
        async Task GoTo(Baum baum)
        {
            if (baum == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(BaumPage)}", true, new Dictionary<string, object>
            {
                {"Baum", baum }
            });
        }

        /// <summary>
        /// Loeschen eines Baumes
        /// </summary>
        /// <param name="baum"></param>
        /// <returns></returns>
        [RelayCommand]
        async Task DeleteBaum(Baum baum)
        {
            bool answer = await Shell.Current.DisplayAlert("Baum löschen", "Wollen sie den Baum wirklich löschen?", "Ja", "Nein");

            if (answer)
            {
                if (IsBusy)
                    return;

                try
                {
                    IsBusy = true;
                    await baumService.DeleteBaum(baum);

                    var idx = BaumListe.IndexOf(BaumListe.Where(a => a.ID == baum.ID).FirstOrDefault());
                    if (idx >= 0)
                        BaumListe.RemoveAt(idx);

                    idx = baumListeBackup.IndexOf(baumListeBackup.Where(a => a.ID == baum.ID).FirstOrDefault());
                    if (idx >= 0)
                        baumListeBackup.RemoveAt(idx);

                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Fehler beim Löschen des Baumes", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                    IsRefreshing = false;
                }
            }
            return;
        }
        #endregion

        #region Methoden aus View
        public override void OnAppearing()
        {
            GetBaeume();
            base.OnAppearing();
        }

        #endregion

    }
}
