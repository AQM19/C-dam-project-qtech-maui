using _2.BusinessLogic;
using _4.Entities;
using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace _3.Presentation;

public partial class RegisterPage : ContentPage
{
    private string _filename;
    public RegisterPage()
    {
        InitializeComponent();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    _filename = ImageSource.FromStream(() => stream).ToString();
                }
            }

            return;
        }
        catch (Exception)
        {
            throw;
        }
    }
    private async void BtnLogin_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("LoadingPage");
    }

    private async void BtnRegister_Clicked(object sender, EventArgs e)
    {
        if (await ValidData())
        {
            byte[] salt = GenerarSalt();
            string password = GenerarContra(salt);
            string username = UsernameEntryRegister.Text.ToLower();

            await CrearContainerBlobAzure(username);
            string profilePic = await CargarImagenPerfilAzure(username);

            Usuario newRegisterUser = new Usuario
            {
                NombreUsuario = username,
                Email = EmailEntryRegister.Text,
                Salt = salt,
                Contrasena = password,
                FotoPerfil = profilePic,
                Perfil = "CLIENTE"
            };

            try
            {
                Herramientas.CreateUsuario(newRegisterUser);
                App.Usuario = newRegisterUser;

                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
            catch (Exception)
            {
                await DisplayAlert("Error de conexión", "No se pudo establecer una conexión con el servidor. Por favor, inténtelo de nuevo más tarde.", "OK");
            }
        }
    }

    private async Task<string> CargarImagenPerfilAzure(string username)
    {
        if (!string.IsNullOrEmpty(_filename))
        {
            Uri blobUri = new Uri($"https://qtechstorage.blob.core.windows.net/{username}/profile_pic{Path.GetExtension(_filename)}");
            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential("qtechstorage", App.Configuration["Settings:qtechstorage"]);
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            FileStream fileStream = File.OpenRead(Path.GetFullPath(_filename));
            await blobClient.UploadAsync(fileStream, true);
            fileStream.Close();

            if (await Task.FromResult(true))
            {
                return blobUri.ToString();
            }
        }

        return string.Empty;
    }

    private async Task<BlobContainerClient> CrearContainerBlobAzure(string username)
    {
        try
        {
            string connectionString = App.Configuration["Settings:azureacc"].ToString();
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            string containerName = username;

            BlobContainerClient container = await blobServiceClient.CreateBlobContainerAsync(containerName);
            container.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            if (await container.ExistsAsync())
            {
                return container;
            }

            return null;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private string GenerarContra(byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(PasswordEntryRegister.Text);

        byte[] inputBytes = passwordBytes.Concat(salt).ToArray();

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passHash = sha256.ComputeHash(inputBytes);
            string hash = Convert.ToBase64String(passHash);
            return hash;
        }
    }

    private byte[] GenerarSalt()
    {
        int saltLength = 32;
        byte[] saltBytes = new byte[saltLength];

        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(saltBytes);
        }

        return saltBytes;
    }

    private async Task<bool> ValidData()
    {
        try
        {
            if (string.IsNullOrEmpty(UsernameEntryRegister.Text))
            {
                await DisplayAlert("Error", "El campo usuario no puede estar vacío.", "OK");
                UsernameEntryRegister.Focus();
                return false;
            }
            if (!await Herramientas.ComprobarUsuario(UsernameEntryRegister.Text))
            {
                await DisplayAlert("Aviso", "Ese nombre de usuario ya está en uso.", "OK");
                UsernameEntryRegister.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(EmailEntryRegister.Text))
            {
                await DisplayAlert("Error", "El campo email no puede estar vacío.", "OK");
                EmailEntryRegister.Focus();
                return false;
            }
            if (!await Herramientas.ComprobarUsuario(EmailEntryRegister.Text))
            {
                await DisplayAlert("Aviso", "Ese email ya está en uso.", "OK");
                EmailEntryRegister.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(PasswordEntryRegister.Text))
            {
                await DisplayAlert("Error", "El campo contraseña no puede estar vacío.", "OK");
                PasswordEntryRegister.Focus();
                return false;
            }

            string codeConfirmation = GenerateRandomCode();
            EmailSender.SendVerificationEmail(EmailEntryRegister.Text, codeConfirmation, App.Configuration["Settings:remitent"], App.Configuration["Settings:passremitent"]);
            string verification = await DisplayPromptAsync("Verificación", "Ingresa el código de verificación que le hemos mandado al correo.");

            if (!verification.Equals(codeConfirmation))
            {
                await DisplayAlert("Error de registro", "No se ha podido confirmar la cuenta.", "OK");
                return false;
            }
        }
        catch (Exception)
        {
            await DisplayAlert("Error de conexión", "No se pudo establecer una conexión con el servidor. Por favor, inténtelo de nuevo más tarde.", "OK");
            return false;
        }

        return true;
    }

    private string GenerateRandomCode()
    {
        string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        Random random = new Random();
        StringBuilder code = new StringBuilder();

        for (int i = 0; i < 6; i++)
        {
            code.Append(characters[random.Next(characters.Length)]);
        }

        return code.ToString();
    }
}