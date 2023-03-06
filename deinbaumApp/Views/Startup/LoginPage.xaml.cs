using deinbaumApp.ViewModel.Startup;

namespace deinbaumApp.Views.Startup;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel viewModel)
	{
		InitializeComponent();

        InitializeComponent();
        this.BindingContext = viewModel;
    }
}