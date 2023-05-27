using _2.BusinessLogic;
using _4.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace _3.Presentation;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        if (await ValidData())
        {
            Loader.IsRunning = true;
            Loader.IsVisible = true;
            BtnLogin.IsEnabled = false;
            UsernameEntry.IsEnabled = false;
            PasswordEntry.IsEnabled = false;

            Usuario usuario;

            try
            {
                usuario = await Herramientas.Login(UsernameEntry.Text, PasswordEntry.Text);
            }
            catch (Exception)
            {
                await DisplayAlert("Error de conexi�n", "No se pudo establecer una conexi�n con el servidor. Por favor, int�ntelo de nuevo m�s tarde.", "OK");

                Loader.IsRunning = false;
                Loader.IsVisible = false;
                BtnLogin.IsEnabled = true;
                UsernameEntry.IsEnabled = true;
                PasswordEntry.IsEnabled = true;

                return;
            }

            if (usuario == null)
            {
                await DisplayAlert("Aviso", "Nombre de usuario o contrase�a incorrectos.", "OK");
                UsernameEntry.Text = string.Empty;
                PasswordEntry.Text = string.Empty;

                Loader.IsRunning = false;
                Loader.IsVisible = false;
                BtnLogin.IsEnabled = true;
                UsernameEntry.IsEnabled = true;
                PasswordEntry.IsEnabled = true;

                return;
            }

            Loader.IsRunning = false;
            Loader.IsVisible = false;
            BtnLogin.IsEnabled = true;
            UsernameEntry.IsEnabled = true;
            PasswordEntry.IsEnabled = true;

            App.Usuario = usuario;

            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }

        return;
    }
    private async void BtnRegistro_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("RegisterPage");
    }

    private async Task<bool> ValidData()
    {
        if (string.IsNullOrEmpty(UsernameEntry.Text))
        {
            await DisplayAlert("Error", "El campo usuario no puede estar vac�o.", "OK");
            UsernameEntry.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(PasswordEntry.Text))
        {
            await DisplayAlert("Error", "El campo contrase�a no puede estar vac�o.", "OK");
            PasswordEntry.Focus();
            return false;
        }

        return true;
    }
}