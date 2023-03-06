namespace deinbaumApp.Controls;

/// <summary>
/// CodeBehind Um bei Flyoutitem Header den eingeloggten
/// User mit der zugewiesenen Rollen anzuzeigen
/// </summary>
public partial class FlyoutHeaderControl : StackLayout
{
    public FlyoutHeaderControl()
    {
        InitializeComponent();

        if (App.UserDetails != null)
        {
            lblUserName.Text = App.UserDetails.Login;
            lblUserRole.Text = App.UserDetails.RoleText;
        }
    }
}