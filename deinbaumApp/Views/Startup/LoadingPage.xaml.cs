using deinbaumApp.ViewModel.Startup;

namespace deinbaumApp.Views.Startup;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}