using _3.Presentation._3.ViewModel;
using _4.Entities;

namespace _3.Presentation;

public partial class BuscadorPage : ContentPage
{
    private readonly Usuario _usuario;
    private BuscadorViewModel ViewModel { get; set; }

    public BuscadorPage()
	{
		InitializeComponent();

        _usuario = App.Usuario;
        this.ViewModel = new BuscadorViewModel();
        this.BindingContext = ViewModel;
    }

    private async void PerformSearchCommand(object sender, EventArgs e)
    {
        string query = searchBar.Text;
        ViewModel.PerformSearch.Execute(query);
    }
}