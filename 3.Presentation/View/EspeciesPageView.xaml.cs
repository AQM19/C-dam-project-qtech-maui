using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class EspeciesPageView : ContentPage
{
	private readonly Usuario _usuario;
	private EspeciesViewModel ViewModel { get; set; }
    private Action<Especie> OnItemSelected { get; set; }
    public EspeciesPageView()
	{
		InitializeComponent();
	}
	
	public EspeciesPageView(List<Especie> especiesTerrario, Action<Especie> onItemSelected): this()
	{
        _usuario = App.Usuario;
        this.ViewModel = new EspeciesViewModel();
        this.BindingContext = ViewModel;
        this.OnItemSelected = onItemSelected;
        CargarEspecies(especiesTerrario);
    }

	private async void CargarEspecies(List<Especie> especiesTerrario)
	{
		this.ViewModel.Especies = this.ViewModel.Especies ?? new ObservableCollection<Especie>();
		this.ViewModel.Especies.Clear();

		List<Especie> especies = await Herramientas.GetEspeciesPosibles(especiesTerrario);

		if(especies.Count > 0)
		{
			this.ViewModel.Especies = new ObservableCollection<Especie>(especies);
		}
	}

    private async void LvEspecies_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Especie especie)
        {
            OnItemSelected?.Invoke(especie);
			await Navigation.PopAsync();
			
        }
    }
}