using _2.BusinessLogic;
using _3.Presentation._2.View;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation;

public partial class PerfilPage : ContentPage
{
    public PerfilPage()
    {
        InitializeComponent();

        Usuario usuario = App.Usuario;
        this.BindingContext = usuario;

        ObtenerInfo(usuario);
    }

    private async void ObtenerInfo(Usuario usuario)
    {
        TerrariosViewModel viewModel = new TerrariosViewModel();
        ObservableCollection<Terrario> terrarios = await ObtenerTerrarios(usuario.Id);
        viewModel.Terrarios = terrarios;
        LvTerrarios.BindingContext = viewModel;
    }


    private async Task<ObservableCollection<Terrario>> ObtenerTerrarios(long userId)
    {
        List<Terrario> terrarios = await Herramientas.GetTerrariosUsuario(userId);
        return new ObservableCollection<Terrario>(terrarios);
    }

    private async void LvTerrarios_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Terrario selectedTerra = e.Item as Terrario;

        Navigation.PushAsync(new TerrarioPageView(selectedTerra));
    }
}