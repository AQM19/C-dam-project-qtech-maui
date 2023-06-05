using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _3.Presentation.View;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class TareasPageView : ContentPage
{
	private Terrario _terrario;
	public TareasPageView()
	{
		InitializeComponent();
	}

	public TareasPageView(Terrario terrario) : this()
	{
		_terrario = terrario;
		CargarInfo();
	}

	private async void CargarInfo()
	{
        TareasViewModel viewModel = new TareasViewModel();
        this.BindingContext = viewModel;

		viewModel.Tareas = viewModel.Tareas ?? new ObservableCollection<Tarea>();
		viewModel.Tareas.Clear();

		viewModel.Tareas = await CargarTareas();
    }

	private async Task<ObservableCollection<Tarea>> CargarTareas()
	{
        List<Tarea> tareas = await Herramientas.GetTareasByTerra(_terrario.Id);
        return new ObservableCollection<Tarea>(tareas);
    }

    private async void OnItemSelected(Tarea tarea)
	{
        if (tarea.Id > 0)
        {
            await Herramientas.UpdateTarea(tarea.Id, tarea);
        }
        else
        {
            await Herramientas.CreateTarea(tarea);
        }
        CargarInfo();
    }


    private async void AddTarea_Clicked(object sender, EventArgs e)
    {
		Tarea tarea = new Tarea();
        await Navigation.PushAsync(new AddTareaPageView(tarea, OnItemSelected));
    }

    private async void LvTareas_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		Tarea tarea = (Tarea)LvTareas.SelectedItem;
        await Navigation.PushAsync(new AddTareaPageView(tarea, OnItemSelected));
    }
}