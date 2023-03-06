using deinBaum.Lib.PersonDaten;
using deinbaumApp.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel um Waldeigentuemer zu erfassen oder zu bearbeiten
    /// </summary>
    [QueryProperty(nameof(Waldeigentuemer), "Waldeigentuemer")]
    public partial class WaldeigentuemerRegisterViewModel : BaseViewModel
    {
        WaldeigentuemerService waldeigentuemerService;

        [ObservableProperty]
        private Waldeigentuemer waldeigentuemer = new();

        [ObservableProperty]
        private bool isUpdateMode = false;

        [ObservableProperty]
        private string vorname = string.Empty;
        [ObservableProperty]
        private string name = string.Empty;
        [ObservableProperty]
        private int? plz = null;
        [ObservableProperty]
        private string ort = string.Empty;
        [ObservableProperty]
        private string email = string.Empty;
        [ObservableProperty]
        private string mobileNr = string.Empty;

        /// <summary>
        /// Konstruktor mit Dependency Injection
        /// </summary>
        /// <param name="waldeigentuemerService"></param>
        public WaldeigentuemerRegisterViewModel(WaldeigentuemerService waldeigentuemerService)
        {
            Title = "Waldeigentümer erfassen";
            this.waldeigentuemerService = waldeigentuemerService;
        }

        #region Properties Changed Methods

        /// <summary>
        /// Setzen ObservableProperties mit neuem Waldeigentuemer
        /// </summary>
        /// <param name="value"></param>
        partial void OnWaldeigentuemerChanged(Waldeigentuemer value)
        {
            IsUpdateMode = true;
            Title = "Waldeigentümer bearbeiten";

            Vorname = value.Vorname;
            Name = value.Name;
            Plz = value.PLZ;
            Ort = value.Ort;
            Email = value.Email;
            MobileNr = value.MobileNr;
        }

        #endregion


        #region Commands

        /// <summary>
        /// Erfassen oder Aktualisieren eines Waldeigentuemers
        /// </summary>
        [RelayCommand]
        async void Register()
        {
            if (IsBusy)
                return;

            if (!string.IsNullOrWhiteSpace(Vorname)
                && !string.IsNullOrWhiteSpace(Name)
                && Plz is not null
                && !string.IsNullOrWhiteSpace(Ort)
                && !string.IsNullOrWhiteSpace(Email))
            {
                IsBusy = true;

                var waldeigentuemer = new Waldeigentuemer
                {
                    Vorname = Vorname,
                    Name = Name,
                    PLZ = (int)Plz,
                    Ort = Ort,
                    Email = Email
                };
                waldeigentuemer.MobileNr = MobileNr;
                waldeigentuemer.ID = Waldeigentuemer.ID;


                if (IsUpdateMode)
                {
                    // Put
                    await waldeigentuemerService.Update(waldeigentuemer);
                }
                else
                {
                    // Post
                    await waldeigentuemerService.Register(waldeigentuemer);
                }
            }

            IsBusy = false;
        }

        #endregion
    }
}
