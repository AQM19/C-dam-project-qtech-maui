using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _3.Presentation.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class UserTerrarioPageView : ContentPage
{
    private Usuario _contacto;
    private Terrario _terrario;
    private Visita _visita;
    private Timer _timer;
    private bool _isCreated = false;
    public UserTerrarioPageView()
    {
        InitializeComponent();
    }

    public UserTerrarioPageView(Usuario contacto, Terrario terrario) : this()
    {
        _contacto = contacto;
        _terrario = terrario;

        _visita = new Visita
        {
            Idterrario = _terrario.Id,
            Idusuario = _terrario.Idusuario,
            Fecha = DateTime.Now,
        };

        this.BindingContext = _terrario;
        _timer = new Timer(async (_) => await ObtenerValoresTerrario(), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
        CargarEspecies();
        CargarComentarios();
    }

    private async Task ObtenerValoresTerrario()
    {
        Lectura lectura = await Herramientas.GetLecturaActual(_terrario.Id);

        if (lectura != null)
        {
            ProgressBarTemperature.Progress = (lectura.Temperatura / 100);
            ProgressBarHumedad.Progress = (lectura.Humedad / 100);
            ProgressBarLuz.Progress = (lectura.Luz / 100);
        }
    }

    private async void CargarEspecies()
    {
        EspeciesViewModel viewModel = new EspeciesViewModel();
        LvEspecies.BindingContext = viewModel;

        viewModel.Especies = viewModel.Especies ?? new ObservableCollection<Especie>();
        viewModel.Especies.Clear();

        List<Especie> especies = await Herramientas.GetEspeciesTerrario(_terrario.Id);
        viewModel.Especies = new ObservableCollection<Especie>(especies);
    }

    private async void CargarComentarios()
    {
        VisitasViewModel viewModel = new VisitasViewModel();
        LvVisitas.BindingContext = viewModel;

        viewModel.Visitas = viewModel.Visitas ?? new ObservableCollection<Visita>();
        viewModel.Visitas.Clear();

        List<Visita> visitas = await Herramientas.GetVisitas(_terrario.Id);
        viewModel.Visitas = new ObservableCollection<Visita>(visitas);
    }

    protected async override void OnDisappearing()
    {
        base.OnDisappearing();
        if (!_isCreated)
        {
            await Herramientas.CreateVisita(_visita);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        _visita.Comentario = await DisplayPromptAsync("Comentario", "Ingresa tu comentario.");
        await Herramientas.CreateVisita(_visita);
        _isCreated = true;
        CargarComentarios();
    }
}