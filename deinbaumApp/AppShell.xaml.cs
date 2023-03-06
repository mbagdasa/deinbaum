using deinbaumApp.Views.Dashboard;

namespace deinbaumApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        this.BindingContext = new AppShellViewModel();

        // Registrierung der Navigationsrouten
        Routing.RegisterRoute(nameof(BaumPage), typeof(BaumPage));
        Routing.RegisterRoute(nameof(MitarbeiterRegisterPage), typeof(MitarbeiterRegisterPage));
        Routing.RegisterRoute(nameof(MitarbeiterPage), typeof(MitarbeiterPage));
        Routing.RegisterRoute(nameof(WaldeigentuemerRegisterPage), typeof(WaldeigentuemerRegisterPage));
        Routing.RegisterRoute(nameof(WaldeigentuemerPage), typeof(WaldeigentuemerPage));
        Routing.RegisterRoute(nameof(MapSuiViewPage), typeof(MapSuiViewPage));
    }
}
