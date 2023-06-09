using _2.BusinessLogic;
using _3.Presentation._2.View;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace _3.Presentation;

public partial class DashboardPage : ContentPage
{
    private readonly Usuario _usuario;

    public DashboardPage()
    {
        InitializeComponent();

        _usuario = App.Usuario;
        StartNotificationPolling();
        CargarTerrariosAsync();
    }

    private async void StartNotificationPolling()
    {
        await NotificationPollingComponent.StartPeriodicQuery(_usuario.Id);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarTerrariosAsync();
    }

    private async void CargarTerrariosAsync()
    {
        TerrariosViewModel viewModel = new TerrariosViewModel();
        this.BindingContext = viewModel;

        viewModel.Terrarios = viewModel.Terrarios ?? new ObservableCollection<Terrario>();
        viewModel.Terrarios.Clear();

        List<Terrario> terrarios = await Herramientas.GetTerrariosUsuario(_usuario.Id);

        if (terrarios.Count > 0)
        {
            foreach (Terrario t in terrarios)
            {
                viewModel.Terrarios.Add(t);
            };
        };
    }

    private async void LvTerrarios_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Terrario selectedTerra = e.Item as Terrario;
        await Navigation.PushAsync(new TerrarioPageView(selectedTerra, null, OnTerraDeleted));
        CargarTerrariosAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        Terrario terrario = new Terrario();
        await Navigation.PushAsync(new TerrarioPageView(terrario, OnTerraCreated, null));
        CargarTerrariosAsync();
    }

    private async void OnTerraCreated(Terrario terrario)
    {
        await Herramientas.CreateTerrario(terrario);
        CargarTerrariosAsync();
    }

    private async void OnTerraDeleted(Terrario terrario)
    {
        await Herramientas.DeleteTerrario(terrario.Id);
        CargarTerrariosAsync();
    }
}