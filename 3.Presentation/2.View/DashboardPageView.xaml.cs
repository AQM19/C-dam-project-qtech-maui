using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace _3.Presentation;

public partial class DashboardPage : ContentPage
{
	private readonly Usuario _usuario;
    private DashboardViewModel ViewModel { get; set; }

    public DashboardPage()
	{
		InitializeComponent();

        _usuario = App.Usuario;
        this.ViewModel = new DashboardViewModel();
        this.BindingContext = this.ViewModel;
        CargarTerrariosAsync();
    }

	private async void CargarTerrariosAsync()
	{
        this.ViewModel.Terrarios = this.ViewModel.Terrarios ?? new ObservableCollection<Terrario>();
        this.ViewModel.Terrarios.Clear();

        List<Terrario> terrarios = await Herramientas.GetTerrariosUsuario(_usuario.Id);

        if (terrarios.Count > 0)
        {
            foreach (Terrario t in terrarios)
            {
                this.ViewModel.Terrarios.Add(t);
            };
        };
    }
}