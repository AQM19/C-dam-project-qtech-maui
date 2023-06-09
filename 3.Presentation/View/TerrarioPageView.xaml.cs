using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _3.Presentation.View;
using _4.Entities;
using Azure.Storage.Blobs;
using Azure.Storage;
using Microsoft.Extensions.Configuration;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class TerrarioPageView : ContentPage
{
    private Terrario _terrario;
    private bool _imageChanged = false;
    private MemoryStream _imageStream;
    private Action<Terrario> OnTerraCreated { get; set; }
    private Action<Terrario> OnTerraDeleted { get; set; }
    public TerrarioPageView()
    {
        InitializeComponent();
    }

    public TerrarioPageView(Terrario terrario, Action<Terrario> onTerraCreated, Action<Terrario> onTerraDeleted) : this()
    {
        _terrario = terrario;
        this.BindingContext = _terrario;
        this.OnTerraCreated = onTerraCreated;
        this.OnTerraDeleted = onTerraDeleted;

        if (_terrario.Id == 0)
        {
            AddEspecie.IsEnabled = false;
            DelEspecie.IsEnabled = false;
            ButtonObservaciones.IsEnabled = false;
            ButtonTareas.IsEnabled = false;
            ButtonLecturas.IsEnabled = false;
        }

        ObtenerInfo(_terrario);
    }

    private async void ObtenerInfo(Terrario terrario)
    {
        ObservableCollection<Especie> especies = await ObtenerEspecies();
        EspeciesViewModel viewModel = new EspeciesViewModel();
        viewModel.Especies = especies;
        LvEspecies.BindingContext = viewModel;
    }

    private async Task<ObservableCollection<Especie>> ObtenerEspecies()
    {
        List<Especie> especies = await Herramientas.GetEspeciesTerrario(_terrario.Id);
        return new ObservableCollection<Especie>(especies);
    }

    private void Checkbox_Private_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Contra.IsVisible = Checkbox_Private.IsChecked;
        RepContra.IsVisible = Checkbox_Private.IsChecked;
    }

    private void AddEspecie_Clicked(object sender, EventArgs e)
    {
        EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
        Navigation.PushAsync(new EspeciesPageView(viewModel.Especies.ToList(), OnItemSelected));
    }

    private async void OnItemSelected(Especie especie)
    {
        EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
        ObservableCollection<Especie> especies = viewModel.Especies;

        especies.Add(especie);
    }

    private async void DelEspecie_Clicked(object sender, EventArgs e)
    {
        if (LvEspecies.SelectedItem != null)
        {
            Especie especieSeleccionada = (Especie)LvEspecies.SelectedItem;

            EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
            ObservableCollection<Especie> especies = viewModel.Especies;

            especies.Remove(especieSeleccionada);

            LvEspecies.SelectedItem = null;
        }
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (await ValidData())
        {
            _terrario = (Terrario)this.BindingContext;

            if (_terrario.Id == 0)
            {
                CreateTerrario();
                return;
            }

            EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
            List<Especie> especies = viewModel.Especies.ToList();
            List<EspecieTerrario> especieTerrarios = new List<EspecieTerrario>();

            for (int i = 0; i < especies.Count; i++)
            {
                especieTerrarios.Add(new EspecieTerrario
                {
                    Idespecie = especies[i].Id,
                    Idterrario = _terrario.Id,
                    FechaInsercion = DateTime.Today
                });
            }

            if (_imageChanged)
            {
                _terrario.Foto = CambiarImagenTerrario();
            }

            await Herramientas.UpdateEspeciesOfTerrario(_terrario.Id, especieTerrarios);
            await Herramientas.UpdateTerrario(_terrario.Id, _terrario);

            await Navigation.PopAsync();
        }
    }

    private string CambiarImagenTerrario()
    {
        if (_imageChanged == false || _imageStream != null)
        {
            string containerName = App.Usuario.NombreUsuario.ToLower();
            string imageName = _terrario.Nombre;
            string uri = $"https://qtechstorage.blob.core.windows.net/{containerName}/{imageName}.jpg".Replace(" ", "").Trim();

            Uri blobUri = new Uri(uri);
            StorageSharedKeyCredential storageCredentials = new StorageSharedKeyCredential("qtechstorage", App.Configuration["Settings:qtechstorage"]);
            BlobClient blobClient = new BlobClient(blobUri, storageCredentials);

            _imageStream.Position = 0;
            blobClient.Upload(_imageStream, true);

            return blobUri.ToString();
        }

        return ImagePic.Source.ToString();
    }

    private async void CreateTerrario()
    {
        if (_imageChanged)
        {
            _terrario.Foto = CambiarImagenTerrario();
        }

        _terrario.Idusuario = App.Usuario.Id;
        _terrario.FechaCreacion = DateTime.Now;

        OnTerraCreated?.Invoke(_terrario);
        await Navigation.PopAsync();
    }

    private async Task<bool> ValidData()
    {
        if (string.IsNullOrEmpty(EntryName.Text))
        {
            await DisplayAlert("Error", "El campo nombre no puede estar vacío", "Ok");
            EntryName.Focus();
            return false;
        }
        if (!string.IsNullOrEmpty(EntryTamanio.Text) && int.Parse(EntryTamanio.Text) <= 0)
        {
            await DisplayAlert("Error", "El terrario no puede tener un tamaño negativo", "Ok");
            EntryTamanio.Focus();
            return false;
        }
        if (Checkbox_Private.IsChecked == true && string.IsNullOrEmpty(_terrario.Contrasena))
        {
            await DisplayAlert("Error", "El terrario debe tener una contraseña si es privado", "Ok");
            Checkbox_Private.Focus();
            return false;
        }
        return true;
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        if (_terrario.Id > 0)
        {
            bool answer = await DisplayAlert("Borrado", "¿Estás seguro de querer borrar el terrario?", "Si", "No");

            if (answer)
            {
                OnTerraDeleted?.Invoke(_terrario);
            }
        }

        await Navigation.PopAsync();
    }

    private void ButtonObservaciones_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ObservacionesPageView(_terrario));
    }

    private void ButtonTareas_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new TareasPageView(_terrario));
    }

    private void ButtonLecturas_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new LecturasTerrarioPageView(_terrario));
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
                    var stream = await result.OpenReadAsync();
                    _imageStream = new MemoryStream();
                    await stream.CopyToAsync(_imageStream);
                    _imageStream.Position = 0;
                    ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(_imageStream.ToArray()));
                    ImagePic.Source = imageSource;
                    _imageChanged = true;
                }
            }

            return;
        }
        catch (Exception)
        {
            throw;
        }
    }
}