namespace deinbaumApp.Views.Dashboard;

public partial class WaldeigentuemerPage : ContentPage
{
	public WaldeigentuemerPage(WaldeigentuemerViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}