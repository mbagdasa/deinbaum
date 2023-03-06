using deinBaum.Lib.BaumStruktur;
using deinbaumApp.Services;
using deinbaumApp.ViewModel;
using InputKit.Shared.Validations;
using UraniumUI.Material.Controls;

namespace deinbaumApp.Views.Dashboard;

public partial class BaumPage : ContentPage
{
    public BaumPage(BaumDetailViewModel viewModel)
	{
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        SetupBinding(BindingContext);
    }

    protected override void OnDisappearing()
    {

        TearDownBinding(BindingContext);
        base.OnDisappearing();
    }

    protected void SetupBinding(object bindingContext)
    {
        if (bindingContext is BaseViewModel vm)
        {
            vm.OnAppearing();
        }
    }

    protected void TearDownBinding(object bindingContext)
    {
        if (bindingContext is BaseViewModel vm)
        {
            vm.OnDisappearing();
        }
    }
}
