using deinbaumApp.ViewModel;

namespace deinbaumApp.Views.Dashboard;

public partial class BaeumePage : ContentPage
{

    public BaeumePage(BaeumeAllViewModel  viewModel)
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