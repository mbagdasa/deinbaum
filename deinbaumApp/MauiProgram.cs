using UraniumUI;
using CommunityToolkit.Maui;
using deinbaumApp.Services;
using deinbaumApp.ViewModel;
using deinbaumApp.Views.Dashboard;
using deinbaumApp.Views.Startup;
using deinbaumApp.ViewModel.Startup;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Maps;

namespace deinbaumApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
             .UseMauiApp<App>()
             .UseMauiCommunityToolkit()
             .UseUraniumUI()
             .ConfigureMauiHandlers(handlers =>
             {
                 handlers.AddUraniumUIHandlers();
             })
             .ConfigureFonts(fonts =>
             {
                 fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                 fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                 fonts.AddFontAwesomeIconFonts();
                 fonts.AddMaterialIconFonts();
             });

        //Views
        builder.Services.AddSingleton<AboutPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<BaeumePage>();
        builder.Services.AddTransient<MitarbeiterRegisterPage>();
        builder.Services.AddTransient<MitarbeiterPage>();
        builder.Services.AddTransient<WaldeigentuemerRegisterPage>();
        builder.Services.AddTransient<WaldeigentuemerPage>();
        builder.Services.AddSingleton<BaumArtVerwaltungPage>();
        builder.Services.AddSingleton<MapSuiViewPage>();
        builder.Services.AddTransient<BaumPage>();


        // View Models
        builder.Services.AddSingleton<AboutViewModel>();
        builder.Services.AddSingleton<LoginPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddTransient<MitarbeiterRegisterViewModel>();
        builder.Services.AddTransient<MitarbeiterViewModel>();
        builder.Services.AddTransient<WaldeigentuemerRegisterViewModel>();
        builder.Services.AddTransient<WaldeigentuemerViewModel>();
        builder.Services.AddSingleton<BaumArtViewModel>();        
        builder.Services.AddSingleton<BaeumeAllViewModel>();
        builder.Services.AddTransient<BaumDetailViewModel>();
        builder.Services.AddTransient<MapViewModel>();

        // Services
        builder.Services.AddSingleton<LoginService>();
        builder.Services.AddSingleton<BaumArtenService>();
        builder.Services.AddSingleton<BaumMerkmaleService>();
        builder.Services.AddSingleton<BaumZustandService>();        
        builder.Services.AddSingleton<BaumService>();
        builder.Services.AddSingleton<UserService>();
        builder.Services.AddSingleton<WaldeigentuemerService>();
        builder.Services.AddSingleton<MapService>();
        

        // Others
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);

        builder.UseMauiApp<App>()
            .UseSkiaSharp(true).ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        return builder.Build();
    }
}