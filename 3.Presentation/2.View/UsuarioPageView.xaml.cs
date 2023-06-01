using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class UsuarioPageView : ContentPage
{
    public UsuarioPageView()
    {
        InitializeComponent();
    }

    public UsuarioPageView(Usuario contacto) : this()
    {
        this.BindingContext = contacto;
        ObtenerInfo(contacto);
    }

    private async void ObtenerInfo(Usuario contacto)
    {
        TerrariosViewModel terrariosViewModel = new TerrariosViewModel();
        LogrosViewModel logrosViewModel = new LogrosViewModel();

        ObservableCollection<Terrario> terrarios = await ObtenerTerrarios(contacto.Id);
        terrariosViewModel.Terrarios = terrarios;
        LvTerrarios.BindingContext = terrariosViewModel;

        ObservableCollection<Logro> logros = await ObtenerLogros(contacto.Id);
        logrosViewModel.Logros = logros;
        LvLogros.BindingContext = logrosViewModel;
    }

    private async Task<ObservableCollection<Terrario>> ObtenerTerrarios(long userId)
    {
        List<Terrario> terrarios = await Herramientas.GetTerrariosUsuario(userId);
        return new ObservableCollection<Terrario>(terrarios);
    }

    private async Task<ObservableCollection<Logro>> ObtenerLogros(long userId)
    {
        List<Logro> logros = await Herramientas.GetLogrosUsuario(userId);
        return new ObservableCollection<Logro>(logros);
    }
}