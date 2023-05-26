using _2.BusinessLogic;
using Microsoft.Extensions.Configuration;

namespace _3.Presentation;

public partial class LoginPage : ContentPage
{
    private readonly IConfiguration _configuration;

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
        //await Shell.Current.GoToAsync("RegisterPage");
        EmailSender.SendVerificationEmail("merigu00@hotmail.com", "¡Este es el mensaje de prueba de verificación de email de Q-Tech AutoTerra!");
    }
}