using deinBaum.Lib.BaumStruktur;
using deinBaum.Lib.FotoStruktur;
using deinbaumApp.Views;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using UraniumUI.Icons.FontAwesome;
using deinbaumApp.Services;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using deinBaum.Lib.PersonDaten;
using deinbaumApp.Views.Dashboard;
using deinbaumApp.Helpers;
using Mapsui.UI.Maui;
using System.Collections.Specialized;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel um einen neuen Baum zu erfassen oder einen bestehenden zu aktualisieren
    /// </summary>
    [QueryProperty(nameof(Baum), "Baum")]
    [QueryProperty(nameof(Mitarbeiter), "Feldmitarbeiter")]
    [QueryProperty(nameof(Waldeigentuemer), "Waldeigentuemer")]
    public partial class BaumDetailViewModel : BaseViewModel
    {
        BaumService baumService;
        BaumArtenService baumArtenService;
        BaumMerkmaleService baumMerkmaleService;
        BaumZustandService baumZustandService;
        IConnectivity connectivity;
        CancellationTokenSource cts;

        [ObservableProperty]
        Baum baum;

        [ObservableProperty]
        ObservableCollection<BaumZustand> baumZustandList = new();


        [ObservableProperty]
        public ObservableCollection<byte[]> baumImages = new();

        [ObservableProperty]
        public Feldmitarbeiter mitarbeiter = new();

        [ObservableProperty]
        public ObservableCollection<Feldmitarbeiter> mitarbeiterListe = new();

        [ObservableProperty]
        public Waldeigentuemer waldeigentuemer = new();

        [ObservableProperty]
        public ObservableCollection<Waldeigentuemer> waldeigentuemerListe = new();

        [ObservableProperty]
        public ObservableCollection<BaumArt> baumArten = new();

        [ObservableProperty]
        public int selectedBaumArtIndex = -1;

        [ObservableProperty]
        public ObservableCollection<BaumMerkmal> baumMerkmaleAll = new();

        [ObservableProperty]
        public ObservableCollection<BaumMerkmal> baumMerkmaleSelection = new();

        [ObservableProperty]
        public ObservableCollection<BaumMerkmal> baumMerkmaleToSelect = new();

        [ObservableProperty]
        public string baumMerkmaleSelectionGroupIcon = "right_arrow.png";

        [ObservableProperty]
        public string baumMerkmaleSelectionText = "noch keine Merkmale ausgewählt";

        [ObservableProperty]
        public FontAttributes baumMerkmaleSelectionTextFontAttr = FontAttributes.Italic;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Durchmesser))]
        double? umfang;

        public double? Durchmesser 
        { 
            get
            {
                if (Umfang is null)
                {
                    return null;
                }
                else
                {
                    return Math.Round((double)(Umfang / Math.PI), 3);
                }
            }
        }

        [ObservableProperty]
        string currentLocationStr;

        Location currentLocation;

        [ObservableProperty]
        bool isSetLocationEnabled = false;

        [ObservableProperty]
        double wGS84_XKoordinaten;

        [ObservableProperty]
        double wGS84_YKoordinaten;

        [ObservableProperty]
        string saveButtonText;

        [ObservableProperty]
        bool isFrameMerkmaleToSelectEnabled = false;

        [ObservableProperty]
        bool isRefreshing;

        /// <summary>
        /// Konstruktor mit Dependency Injection
        /// </summary>
        /// <param name="baumService"></param>
        /// <param name="baumArtenService"></param>
        /// <param name="baumMerkmaleService"></param>
        /// <param name="baumZustandService"></param>
        /// <param name="connectivity"></param>
        public BaumDetailViewModel(BaumService baumService, BaumArtenService baumArtenService, BaumMerkmaleService baumMerkmaleService, BaumZustandService baumZustandService, IConnectivity connectivity)
        {
            this.baumService = baumService;
            this.baumArtenService = baumArtenService;
            this.baumMerkmaleService = baumMerkmaleService;
            this.baumZustandService = baumZustandService;
            this.connectivity = connectivity;

            // neue Bauminstanz erzeugen mit aktuellem Zeitstempel
            Baum = new Baum()
            {
                ErsteErfassung = DateTime.Now
            };

            // Titel setzen
            Title = "Baum erfassen";

            // Button Text setzen
            SaveButtonText = "Baum speichern";

            // Arten in Controls setzen
            FillBaumArten();

            // Merkmale in Controls setzen
            FillBaumMerkmale();

            // Zustaende in Controls setzen
            FillBaumZustaende();

            BaumMerkmaleSelection.CollectionChanged += BaumMerkmaleSelection_CollectionChanged;

        }

        #region Events/Delegates

        /// <summary>
        /// setzt den Bindable-Text um dem User anzuzeigen, ob bereits ein Baummerkaml
        /// ausgewaehlt wurde oder nicht
        /// Wird aufgerufen, sobald Collection aendert
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaumMerkmaleSelection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (BaumMerkmaleSelection?.Count > 0)
            {
                BaumMerkmaleSelectionText = "Ausgewählte Merkmale:";
                BaumMerkmaleSelectionTextFontAttr = FontAttributes.Bold;
            }
            else
            {
                BaumMerkmaleSelectionText = "noch keine Merkmale ausgewählt";
                BaumMerkmaleSelectionTextFontAttr = FontAttributes.Italic;
            }
        }

        #endregion

        #region Properties Changed Methods

        /// <summary>
        /// Bindable Werte fuer den Baum werden neu gesetzt wenn ein neuer Baum zur Bearbeitung ausgewaehlt wurde
        /// </summary>
        /// <param name="value"></param>
        partial void OnBaumChanged(Baum value)
        {
            if (value is null)
            {
                return;
            }

            // Titel setzen
            Title = "Baum bearbeiten";

            // Button Text setzen
            SaveButtonText = "Baum aktualisieren";

            // Set Photos
            if (value.FotoListe?.Count > 0)
            {
                BaumImages.Clear();
                foreach (var foto in value.FotoListe)
                {
                    BaumImages.Add(foto.Fotobytes);
                }
            }

            // Set Feldmitarbeiter
            if (value.Feldmitarbeiter is not null)
            {
                MitarbeiterListe.Clear();
                MitarbeiterListe.Add(value.Feldmitarbeiter);
            }


            // Set Waldeigentuemer
            if (value.Feldmitarbeiter is not null)
            {
                WaldeigentuemerListe.Clear();
                WaldeigentuemerListe.Add(value.Waldeigentuemer);
            }

            // Set Umfang
            Umfang = value.Umfang;

            // Set BaumArt
            var index = BaumArten.IndexOf(BaumArten.Where(a => a.ID == value.Art.ID).FirstOrDefault());
            SelectedBaumArtIndex = index;

            // Set BaumMerkmale
            BaumMerkmaleSelection.Clear();
            if (value.Merkmale?.Count > 0)
            {
                foreach (var item in value.Merkmale)
                {
                    BaumMerkmaleSelection.Add(item);

                    var idx = BaumMerkmaleToSelect.IndexOf(BaumMerkmaleToSelect.Where(a => a.ID == item.ID).FirstOrDefault());
                    if (idx >= 0)
                        BaumMerkmaleToSelect.RemoveAt(idx);
                }
            }

            // Set Zustand
            if (value.ZustandsListe is not null)
            {
                foreach (var item in value.ZustandsListe)
                {
                    var zustand = BaumZustandList
                        .Where(m => m.ID == item.ID).FirstOrDefault();
                    if (zustand is not null)
                    {
                        zustand.IsChecked = true;
                    }
                }
            }

            // Set Position
            WGS84_XKoordinaten = value.WGS84_XKoordinaten;
            WGS84_YKoordinaten = value.WGS84_YKoordinaten;
        }

        /// <summary>
        /// Setzt den neu ausgewaehlten Mitarbeiter
        /// </summary>
        /// <param name="value"></param>
        partial void OnMitarbeiterChanged(Feldmitarbeiter value)
        {
            if (value is null)
            {
                return;
            }
            MitarbeiterListe.Clear();
            MitarbeiterListe.Add(value);
        }

        /// <summary>
        /// Setzt den neu ausgewaehlten Waldeigentuemer
        /// </summary>
        /// <param name="value"></param>
        partial void OnWaldeigentuemerChanged(Waldeigentuemer value)
        {
            if (value is null)
            {
                return;
            }
            WaldeigentuemerListe.Clear();
            WaldeigentuemerListe.Add(value);
        }

        /// <summary>
        /// Steuert grafisches UI-Frame Element
        /// </summary>
        /// <param name="value"></param>
        partial void OnBaumMerkmaleToSelectChanged(ObservableCollection<BaumMerkmal> value)
        {
            if (value is not null && value.Count > 0)
            {
                IsFrameMerkmaleToSelectEnabled = true;
            }
            else
            {
                IsFrameMerkmaleToSelectEnabled = false;
            }
        }
        #endregion

        #region Methoden

        /// <summary>
        /// Baumarten an Observable Property anhaengen
        /// </summary>
        private void FillBaumArten()
        {
            try
            {
                var baumArten = baumArtenService.GetBaumArten();

                if (baumArten is not null && baumArten.Count > 0 && BaumArten.Count != baumArten.Count)
                {
                    BaumArten.Clear();
                    foreach (var art in baumArten)
                        BaumArten.Add(art);
                }

            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }

        }

        /// <summary>
        /// Baummerkmale an Observable Property anhaengen
        /// </summary>
        private void FillBaumMerkmale()
        {
            try
            {
                var bm = baumMerkmaleService.GetBaumMerkmale();

                if (bm is not null && bm.Count > 0 && bm.Count != BaumMerkmaleAll.Count)
                {
                    BaumMerkmaleAll.Clear();
                    foreach (var m in bm)
                        BaumMerkmaleAll.Add(m);
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// BaumZustaende an Observable Property anhaengen
        /// </summary>
        private void FillBaumZustaende()
        {
            try
            {
                var res = baumZustandService.GetBaumZustaende();

                BaumZustandList.Clear();
                foreach (var z in res)
                {
                    z.IsChecked = false;
                    BaumZustandList.Add(z);
                }

            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        /// <summary>
        /// Wandelt die Location in einen String
        /// </summary>
        /// <param name="location"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        string FormatLocation(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"Zurzeit nicht möglich die aktuelle Position zu bestimmen. Exception: {ex?.Message ?? string.Empty}";
            }

            var notAvailable = "Nicht verfügbar";

            return
                $"Latitude: {location.Latitude}\n" +
                $"Longitude: {location.Longitude}\n" +
                $"Horizontale Genauigkeit: {location.Accuracy} m \n" +
                $"Höhe: {(location.Altitude.HasValue ? location.Altitude.Value.ToString() + " m.ü.M" : notAvailable)}\n" +
                $"Höhe Genauigkeit: {(location.VerticalAccuracy.HasValue ? location.VerticalAccuracy.Value.ToString() + " m" : notAvailable)}\n";
        }
        #endregion

        #region Commands

        /// <summary>
        /// Weiterleitung zu Mitarbeiterseite
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task GoToMitarbeiterPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MitarbeiterPage)}?SelectMode={true}");
        }

        /// <summary>
        /// Weiterleitung zu Waldeigentuemerseite
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task GoToWaldeigentuemerPage()
        {
            await Shell.Current.GoToAsync($"{nameof(WaldeigentuemerPage)}?SelectMode={true}");
        }

        /// <summary>
        /// Weiterleitung zur Karte
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task GoToMap()
        {
            await Shell.Current.GoToAsync($"{nameof(MapSuiViewPage)}?Latitude={WGS84_XKoordinaten}&Longitude={WGS84_YKoordinaten}");
        }

        /// <summary>
        /// Klappt die Collection in der View auf oder zu
        /// </summary>
        [RelayCommand]
        public void AddOrRemoveGroupData()
        {
            if (BaumMerkmaleSelectionGroupIcon == "down_arrow.png")
            {
                BaumMerkmaleSelectionGroupIcon = "right_arrow.png";
                BaumMerkmaleToSelect.Clear();
                IsFrameMerkmaleToSelectEnabled = false;
            }
            else
            {
                BaumMerkmaleSelectionGroupIcon = "down_arrow.png";

                var records = baumMerkmaleAll.Where(all => baumMerkmaleSelection.All(s => s.ID != all.ID)).ToList();

                BaumMerkmaleToSelect = new ObservableCollection<BaumMerkmal>(records);
            }
        }

        /// <summary>
        /// Fuegt ein Merkmal zur Collection
        /// </summary>
        /// <param name="bm"></param>
        [RelayCommand]
        public void AddMerkmalToSelection(BaumMerkmal bm)
        {

            // Hinzufuegen
            if (!(BaumMerkmaleSelection.Contains(bm)))
            {
                BaumMerkmaleSelection.Add(bm);
            }

            // Entfernen
            if (BaumMerkmaleToSelect.Contains(bm))
            {
                BaumMerkmaleToSelect.Remove(bm);
            }
        }

        /// <summary>
        /// Entfernt ein Merkmal aus der Collection
        /// </summary>
        /// <param name="bm"></param>
        [RelayCommand]
        public void RemoveMerkmalFromSelection(BaumMerkmal bm)
        {

            // Entfernen
            if (BaumMerkmaleSelection.Contains(bm))
            {
                BaumMerkmaleSelection.Remove(bm);
            }

            // Hinzufuegen
            if (!(BaumMerkmaleToSelect.Contains(bm)))
            {
                if (BaumMerkmaleToSelect.Count == 0 && BaumMerkmaleSelection.Count == BaumMerkmaleAll.Count)
                {
                    BaumMerkmaleToSelect.Add(bm);
                }

            }
        }

        /// <summary>
        /// Erfasst ein Foto und fuegt es zur Collection
        /// </summary>
        [RelayCommand]
        async void DoCapturePhoto()
        {
            var photo = await FotoHelper.DoCapturePhoto();

            if (photo != null)
            {
                BaumImages.Add(photo);
            }
        }

        /// <summary>
        /// Loescht ein Foto aus der Collection
        /// </summary>
        /// <param name="foto"></param>
        [RelayCommand]
        public void DeleteFoto(byte[] foto)
        {
            if (foto != null)
            {
                BaumImages.Remove(foto);
            }
        }

        /// <summary>
        /// Setzt die aktuelle GPS-Position
        /// </summary>
        [RelayCommand]
        async void OnGetCurrentLocation()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                cts = new CancellationTokenSource();
                currentLocation = await Geolocation.GetLocationAsync(request, cts.Token);
                CurrentLocationStr = FormatLocation(currentLocation);
            }
            catch (Exception ex)
            {
                CurrentLocationStr = FormatLocation(null, ex);
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }

            if (currentLocation is not null)
            {
                IsSetLocationEnabled = true;
            }
            else
            {
                IsSetLocationEnabled = false;
            }

            IsBusy = false;
        }

        /// <summary>
        /// Setzt die aktuelle Position als Position des zu erstellenden Baumes
        /// </summary>
        [RelayCommand]
        private void OnSetCurrentLocation()
        {

            if (currentLocation is null)
                return;

            WGS84_XKoordinaten = currentLocation.Latitude;
            WGS84_YKoordinaten = currentLocation.Longitude;
        }

        /// <summary>
        /// Speichert den Baum
        /// </summary>
        [RelayCommand]
        private void SaveBaum()
        {
            // Validierung ob alle Pflichtfelder ausgefuellt sind
            var validateMsg = String.Empty;
            if (Baum.ParzellenNr is null)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Parzellennummer" : ", Parzellennummer";
            if (Baum.Baumhoehe is null)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Baumhöhe" : ", Baumhöhe";
            //if (Umfang is null)
            //    validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Umfang" : ", Umfang";
            if (BaumArten.Where(a => a.ID == SelectedBaumArtIndex).FirstOrDefault() is null)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Baumart" : ", Baumart";
            if (MitarbeiterListe.FirstOrDefault() is null)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Feldmitarbeiter" : ", Feldmitarbeiter";
            if (WaldeigentuemerListe.FirstOrDefault() is null)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Waldeigentümer" : ", Waldeigentümer";
            if (WGS84_XKoordinaten == 0 || WGS84_YKoordinaten == 0)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Koordinaten" : ", Koordinaten";
            if (!string.IsNullOrWhiteSpace(validateMsg))
            {
                AppShell.Current.DisplayAlert("Pflichtfelder nicht ausgefüllt", $"{validateMsg}", "Ok");
                return;
            }

            Baum.LetzteBearbeitung = DateTime.Now;

            // Set Photos
            Baum.FotoListe = new();
            foreach (var foto in BaumImages)
            {
                Baum.FotoListe.Add(new Foto("bildBeschreibung", foto));
            }

            // Set Zustaende
            Baum.ZustandsListe = new();
            foreach (var zustand in (BaumZustandList.Where(a => a.IsChecked == true)))
            {
                Baum.ZustandsListe.Add(zustand);
            }

            // Set Feldmitarbeiter
            Baum.Feldmitarbeiter = MitarbeiterListe.FirstOrDefault();

            // Set Waldeigentuemer
            Baum.Waldeigentuemer = WaldeigentuemerListe.FirstOrDefault();

            // Set Umfang
            Baum.Umfang = Umfang;

            // Set BaumArt
            Baum.Art = BaumArten.Where(a => a.ID == SelectedBaumArtIndex).FirstOrDefault();

            // Set BaumMerkmale
            Baum.Merkmale = new();
            foreach (var merkmal in BaumMerkmaleSelection)
            {
                Baum.Merkmale.Add(merkmal);
            }

            // Set Position
            Baum.WGS84_XKoordinaten = WGS84_XKoordinaten;
            Baum.WGS84_YKoordinaten = WGS84_YKoordinaten;

            if (Baum.ID > 0)
            {
                var response = baumService.UpdateBaum(Baum);
            }
            else
            {
                var response = baumService.SaveBaum(baum);
            }
        }
        #endregion


        #region Methoden aus View
        public override void OnAppearing()
        {
            // Falls BaumArten noch leer, dann befuellen
            if (BaumArten is null || BaumArten.Count == 0)
            {
                FillBaumArten();
            }


            base.OnAppearing();
        }

        public override void OnDisappearing()
        {
            if (IsBusy)
            {
                if (cts != null && !cts.IsCancellationRequested)
                    cts.Cancel();
            }

            base.OnDisappearing();
        }
        #endregion

    }
}
