namespace deinbaumApp.Views.Dashboard;

public partial class MitarbeiterPage : ContentPage
{
	public MitarbeiterPage(MitarbeiterViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}