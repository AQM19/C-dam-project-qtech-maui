using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation._2.View;

public partial class TerrarioPageView : ContentPage
{
	private Terrario _terrario;
	public TerrarioPageView()
	{
		InitializeComponent();
	}

	public TerrarioPageView(Terrario terrario) : this()
    {
		_terrario = terrario;
		this.BindingContext = _terrario;
		ObtenerInfo(_terrario);
    }

	private async void ObtenerInfo(Terrario terrario)
	{
		ObservableCollection<Especie> especies = await ObtenerEspecies();
		EspeciesViewModel viewModel = new EspeciesViewModel();
		viewModel.Especies = especies;
		LvEspecies.BindingContext = viewModel;
	}

	private async Task<ObservableCollection<Especie>> ObtenerEspecies()
	{
		List<Especie> especies = await Herramientas.GetEspeciesTerrario(_terrario.Id);
		return new ObservableCollection<Especie>(especies);
	}

    private void Checkbox_Private_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
		Contra.IsVisible = Checkbox_Private.IsChecked;
		RepContra.IsVisible = Checkbox_Private.IsChecked;
    }

    private void AddEspecie_Clicked(object sender, EventArgs e)
    {
        EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
        Navigation.PushAsync(new EspeciesPageView(viewModel.Especies.ToList(), OnItemSelected));
	}

    private async void OnItemSelected(Especie especie)
    {
        EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
        ObservableCollection<Especie> especies = viewModel.Especies;

		especies.Add(especie);
    }

    private async void DelEspecie_Clicked(object sender, EventArgs e)
    {
		if (LvEspecies.SelectedItem != null)
		{
			Especie especieSeleccionada = (Especie)LvEspecies.SelectedItem;

            EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
            ObservableCollection<Especie> especies = viewModel.Especies;

            especies.Remove(especieSeleccionada);

			LvEspecies.SelectedItem = null;
        }
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        _terrario = (Terrario)this.BindingContext;

        EspeciesViewModel viewModel = (EspeciesViewModel)LvEspecies.BindingContext;
        List<Especie> especies = viewModel.Especies.ToList();
        List<EspecieTerrario> especieTerrarios = new List<EspecieTerrario>();

        for (int i = 0; i < especies.Count; i++)
        {
            especieTerrarios.Add(new EspecieTerrario
            {
                Idespecie = especies[i].Id,
                Idterrario = _terrario.Id,
                FechaInsercion = DateTime.Today
            });
        }

        await Herramientas.UpdateEspeciesOfTerrario(_terrario.Id, especieTerrarios);
        await Herramientas.UpdateTerrario(_terrario.Id, _terrario);

        await Navigation.PopAsync();
    }

    private async void Cancel_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}