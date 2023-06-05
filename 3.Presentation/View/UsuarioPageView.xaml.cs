using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class UsuarioPageView : ContentPage
{
    private Usuario _contacto;
    public UsuarioPageView()
    {
        InitializeComponent();
    }

    public UsuarioPageView(Usuario contacto) : this()
    {
        _contacto = contacto;
        this.BindingContext = _contacto;
        ObtenerInfo(contacto);
    }

    private async void ObtenerInfo(Usuario contacto)
    {
        TerrariosViewModel terrariosViewModel = new TerrariosViewModel();
        LogrosViewModel logrosViewModel = new LogrosViewModel();

        ObservableCollection<Terrario> terrarios = await ObtenerTerrarios();
        terrariosViewModel.Terrarios = terrarios;
        LvTerrarios.BindingContext = terrariosViewModel;

        ObservableCollection<Logro> logros = await ObtenerLogros();
        logrosViewModel.Logros = logros;
        LvLogros.BindingContext = logrosViewModel;
    }

    private async Task<ObservableCollection<Terrario>> ObtenerTerrarios()
    {
        List<Terrario> terrarios = await Herramientas.GetTerrariosUsuario(_contacto.Id);
        return new ObservableCollection<Terrario>(terrarios);
    }

    private async Task<ObservableCollection<Logro>> ObtenerLogros()
    {
        List<Logro> logros = await Herramientas.GetLogrosUsuario(_contacto.Id);
        return new ObservableCollection<Logro>(logros);
    }

    private async void LvTerrarios_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Terrario terrario = (Terrario)LvTerrarios.SelectedItem;
        await Navigation.PushAsync(new UserTerrarioPageView(_contacto, terrario));
    }
}