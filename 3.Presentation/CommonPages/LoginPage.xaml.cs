namespace _3.Presentation;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }


    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DashboardPage");
    }
    private async void BtnRegistro_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }
}