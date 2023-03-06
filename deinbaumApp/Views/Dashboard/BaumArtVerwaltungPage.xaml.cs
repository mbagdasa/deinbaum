namespace deinbaumApp.Views.Dashboard;

public partial class BaumArtVerwaltungPage : ContentPage
{
	public BaumArtVerwaltungPage(BaumArtViewModel viewModel)
	{
		InitializeComponent();
		
		BindingContext = viewModel;
	}
}