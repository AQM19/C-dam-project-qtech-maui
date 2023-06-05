using _2.BusinessLogic;
using _4.Entities;

namespace _3.Presentation.View;

public partial class AddTareaPageView : ContentPage
{
    private Tarea _tarea;
    private Action<Tarea> OnItemSelected { get; set; }
    public AddTareaPageView()
    {
        InitializeComponent();
    }
    public AddTareaPageView(Tarea tarea, Action<Tarea> onItemSelected) : this()
    {
        _tarea = tarea;
        this.OnItemSelected = onItemSelected;
        this.BindingContext = _tarea;
    }

    private async void AddTarea_Clicked(object sender, EventArgs e)
    {
        if (await ValidData())
        {
            _tarea = (Tarea)this.BindingContext;
            OnItemSelected?.Invoke(_tarea);
            await Navigation.PopAsync();
        }
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async Task<bool> ValidData()
    {
        if (string.IsNullOrEmpty(Titulo.Text) || string.IsNullOrWhiteSpace(Titulo.Text))
        {
            await DisplayAlert("Error", "El título no puede estar vacío", "Ok");
            Titulo.Focus();
            return false;
        }
        if (string.IsNullOrEmpty(Descripcion.Text) || string.IsNullOrWhiteSpace(Descripcion.Text))
        {
            await DisplayAlert("Error", "La descripción no puede estar vacía", "Ok");
            Descripcion.Focus();
            return false;
        }
        if (Estado.SelectedItem == null)
        {
            await DisplayAlert("Error", "La tarea debe tener un estado pendiente", "Ok");
            Estado.Focus();
            return false;
        }
        return true;
    }
}