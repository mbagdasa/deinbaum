using deinBaum.Lib.BaumStruktur;
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
using deinBaum.Lib.PersonDaten;
using System.Reflection;
using System.Globalization;
using System.Collections;
using System.IO;
using deinBaum.Lib.FotoStruktur;
using deinbaumApp.Helpers;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel um Mitarbeiter zu erfassen oder zu bearbeiten
    /// </summary>
    [QueryProperty(nameof(Feldmitarbeiter), "Feldmitarbeiter")]
    public partial class MitarbeiterRegisterViewModel : BaseViewModel
    {
        [ObservableProperty]
        private Feldmitarbeiter feldmitarbeiter = new();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(LoginCtrlEnabled))]
        private bool isUpdateMode = false;

        public bool LoginCtrlEnabled { get => !IsUpdateMode; }

        [ObservableProperty]
        private string vorname = string.Empty;
        [ObservableProperty]
        private string name = string.Empty;
        [ObservableProperty]
        private string login = string.Empty;
        [ObservableProperty]
        private string password = string.Empty;
        [ObservableProperty]
        private bool arbeitetNochInDerFirma = false;

        [ObservableProperty]
        public byte[] profilbild;

        [ObservableProperty]
        public ObservableCollection<byte[]> profilbildCV = new();        

        UserService userService;

        /// <summary>
        /// Konstruktor mit Dependency Injection
        /// </summary>
        /// <param name="userService"></param>
        public MitarbeiterRegisterViewModel(UserService userService)
        {
            this.userService = userService;
            Title = "Mitarbeiter registrieren";
        }

        #region Properties Changed Methods

        /// <summary>
        /// Setzen ObservableProperties wenn Feldmitarbeiter aendert
        /// </summary>
        /// <param name="value"></param>
        partial void OnFeldmitarbeiterChanged(Feldmitarbeiter value)
        {
            IsUpdateMode = true;
            Title = "Mitarbeiter bearbeiten";

            Vorname = value.Vorname;
            Name = value.Name;
            Login = value.Login;
            ArbeitetNochInDerFirma = value.ArbeitetNochInDerFirma;
            ProfilbildCV.Clear();
            ProfilbildCV.Add(value.Profilbild);
        }

        #endregion


        #region Commands

        /// <summary>
        /// Speichern oder aktualisieren eines Mitarbeiters
        /// </summary>
        [RelayCommand]
        async void Register()
        {
            if (IsBusy)
                return;

            // Validierung ob alle Pflichtfelder ausgefuellt sind
            var validateMsg = String.Empty;
            if (string.IsNullOrWhiteSpace(Vorname))
                validateMsg= "Vorname";
            if (string.IsNullOrWhiteSpace(Name))
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Name":", Name";
            if (string.IsNullOrWhiteSpace(Login) && LoginCtrlEnabled)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Login" : ", Login";
            if (string.IsNullOrWhiteSpace(Password) && LoginCtrlEnabled)
                validateMsg += string.IsNullOrWhiteSpace(validateMsg) ? "Passwort" : ", Passwort";

            if (!string.IsNullOrWhiteSpace(validateMsg))
            {
                await AppShell.Current.DisplayAlert("Pflichtfelder nicht ausgefüllt", $"{validateMsg}", "Ok");
                return;
            }
            

            IsBusy = true;

            var mitarbeiter = new Feldmitarbeiter
            {
                Vorname = Vorname,
                Name = Name,
                ArbeitetNochInDerFirma = ArbeitetNochInDerFirma,
                Login = Login
            };
            mitarbeiter.ID = Feldmitarbeiter.ID;
            mitarbeiter.Profilbild = ProfilbildCV.FirstOrDefault();

            if (IsUpdateMode)
            {
                // Put Feldmitarbeiter
                await userService.Update(mitarbeiter);
            }
            else
            {
                // Post User
                mitarbeiter.ArbeitetNochInDerFirma = true;

                await userService.Register(
                    mitarbeiter, new User
                    {
                        Login = Login,
                        Password = Password
                    });
            }
            
            IsBusy = false;
        }

        /// <summary>
        /// Erfassung eines Fotos
        /// </summary>
        [RelayCommand]
        async void DoCapturePhoto()
        {
            var photo = await FotoHelper.DoCapturePhoto();

            if (photo != null)
            {
                ProfilbildCV.Clear();
                ProfilbildCV.Add(photo);
            }
        }
        #endregion
    }
}
