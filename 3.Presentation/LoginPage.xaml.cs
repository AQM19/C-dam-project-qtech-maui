namespace _3.Presentation;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }


    private void BtnLogin_Clicked(object sender, EventArgs e)
    {

    }
    private async void BtnRegistro_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }
}