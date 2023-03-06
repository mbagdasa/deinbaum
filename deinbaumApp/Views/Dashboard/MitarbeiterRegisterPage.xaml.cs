namespace deinbaumApp.Views.Dashboard;

public partial class MitarbeiterRegisterPage : ContentPage
{
	public MitarbeiterRegisterPage(MitarbeiterRegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}