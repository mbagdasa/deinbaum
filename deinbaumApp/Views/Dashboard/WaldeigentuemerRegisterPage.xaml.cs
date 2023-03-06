namespace deinbaumApp.Views.Dashboard;

public partial class WaldeigentuemerRegisterPage : ContentPage
{
	public WaldeigentuemerRegisterPage(WaldeigentuemerRegisterViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}