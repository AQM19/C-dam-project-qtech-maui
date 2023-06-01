using _3.Presentation._2.View;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Timers;

namespace _3.Presentation;

public partial class BuscadorPage : ContentPage
{
    private readonly Usuario _usuario;
    private System.Timers.Timer _searchTimer;
    private BuscadorViewModel ViewModel { get; set; }

    public BuscadorPage()
	{
		InitializeComponent();

        _usuario = App.Usuario;
        this.ViewModel = new BuscadorViewModel();
        this.BindingContext = ViewModel;

        _searchTimer = new System.Timers.Timer(500);
        _searchTimer.Elapsed += SearchTimerElapsed;
        _searchTimer.AutoReset = false;
    }

    private void SearchTimerElapsed(object sender, ElapsedEventArgs e)
    {
        string query = searchBar.Text;
        ViewModel.PerformSearch.Execute(query);
    }

    private async void PerformSearchCommand(object sender, EventArgs e)
    {
        _searchTimer.Stop();
        _searchTimer.Start();
    }

    private void LvUsuarios_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Usuario contacto = e.Item as Usuario;

        Navigation.PushAsync(new UsuarioPageView(contacto));
    }
}