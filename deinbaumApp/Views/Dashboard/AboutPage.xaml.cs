namespace deinbaumApp.Views.Dashboard;

public partial class AboutPage : ContentPage
{
	public AboutPage(AboutViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }    
}