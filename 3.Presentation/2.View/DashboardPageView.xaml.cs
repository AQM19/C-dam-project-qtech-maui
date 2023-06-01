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

    private void LvTerrarios_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Terrario selectedTerra = e.Item as Terrario;
        Navigation.PushAsync(new TerrarioPageView(selectedTerra));
    }
}