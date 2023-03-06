using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deinbaumApp.ViewModel
{
    /// <summary>
    /// Viewmodel Basis mit StandardProperties
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

    }
}
