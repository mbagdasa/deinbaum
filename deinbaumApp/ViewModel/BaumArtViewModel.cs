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
using deinBaum.Lib.BaumStruktur;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel um Baumarten zu holen
    /// TODO: Muss noch ansprechend in der View implementiert werden
    /// </summary>
    public partial class BaumArtViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<BaumArt> baumArten = new();

        [ObservableProperty]
        bool isRefreshing;

        BaumArtenService baumArtenService;

        /// <summary>
        /// Konstruktor mit Dependency Injection
        /// </summary>
        /// <param name="baumArtenService"></param>
        public BaumArtViewModel(BaumArtenService baumArtenService)

        {
            this.baumArtenService = baumArtenService;
            GetBaumArten();
        }

        #region Commands

        /// <summary>
        /// Baumarten holen
        /// </summary>
        [RelayCommand]
        async void GetBaumArten()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;


                BaumArten = new ObservableCollection<BaumArt>(await baumArtenService.GetBaumArtenAsync());

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get Baumarten: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }


        }

        /// <summary>
        /// Baumarten erneut holen
        /// </summary>
        [RelayCommand]
        public void Refresh()
        {
            GetBaumArten();
        }

        #endregion


    }
}
