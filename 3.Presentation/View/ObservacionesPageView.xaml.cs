using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class ObservacionesPageView : ContentPage
{
    private Terrario _terrario;
    public ObservacionesPageView()
    {
        InitializeComponent();
    }

    public ObservacionesPageView(Terrario terrario) : this()
    {
        _terrario = terrario;
        CargarInfo();
    }

    private async void CargarInfo()
    {
        ObservacionesViewModel viewModel = new ObservacionesViewModel();
        this.BindingContext = viewModel;

        viewModel.Observaciones = viewModel.Observaciones ?? new ObservableCollection<Observacion>();
        viewModel.Observaciones.Clear();

        viewModel.Observaciones = await CargarObservaciones();
    }

    private async Task<ObservableCollection<Observacion>> CargarObservaciones()
    {
        List<Observacion> observaciones = await Herramientas.GetObservacionesByTerra(_terrario.Id);
        return new ObservableCollection<Observacion>(observaciones);
    }

    private async void AddObservacion_Clicked(object sender, EventArgs e)
    {
        string texto = await DisplayPromptAsync("Observación", "Escribe qué es lo que pasa.");

        if (!string.IsNullOrEmpty(texto) || !string.IsNullOrWhiteSpace(texto))
        {
            Observacion observacion = new Observacion
            {
                Idterrario = _terrario.Id,
                Fecha = DateTime.Now,
                Texto = texto
            };

            await Herramientas.CreateObservacion(observacion);
            CargarInfo();
        }

    }

    private async void LvObservaciones_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Observacion observacion = (Observacion)e.Item;

        string texto = await DisplayPromptAsync("Actualización", observacion.Texto);

        if (!string.IsNullOrEmpty(texto) || !string.IsNullOrWhiteSpace(texto))
        {
            observacion.Texto = texto;
            await Herramientas.UpdateObservacion(observacion.Id, observacion);
            CargarInfo();
        }
    }
}