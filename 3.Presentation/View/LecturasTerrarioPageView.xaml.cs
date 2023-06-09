using _2.BusinessLogic;
using _3.Presentation.Data;
using _4.Entities;

namespace _3.Presentation.View;

public partial class LecturasTerrarioPageView : ContentPage
{
    private Terrario _terrario;
    private List<Lectura> _lecturas;
    public LecturasTerrarioPageView()
    {
        InitializeComponent();
        _lecturas = new List<Lectura>();
    }

    public LecturasTerrarioPageView(Terrario terrario) : this()
    {
        _terrario = terrario;
        CargarInfo();
    }

    private async void CargarInfo()
    {
        _lecturas = await Herramientas.GetLecturasTerrario(_terrario.Id);
        GraphicsDrawable drawable = new GraphicsDrawable { Lecturas = _lecturas };
        graphView.Drawable = drawable;

        if (_lecturas.Count > 0)
        {
            LabGraphStart.Text = _lecturas[0].Fecha.ToShortDateString();
            LabGraphEnd.Text = _lecturas[_lecturas.Count - 1].Fecha.ToShortDateString();

            LabGraphStart.IsVisible = true;
            LabGraphEnd.IsVisible = true;
        } else
        {
            
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "datos.csv");

        using (StreamWriter writer = new StreamWriter(rutaArchivo))
        {
            writer.WriteLine("Columna1,Columna2,Columna3");

            foreach (Lectura l in _lecturas)
            {
                writer.WriteLine(string.Join(";", l));
            }
        }

        // Mostrar mensaje de éxito
        Application.Current.MainPage.DisplayAlert("Archivo CSV creado", $"Se ha creado el archivo CSV en la siguiente ruta:{Environment.NewLine}{rutaArchivo}", "Aceptar");
    }
}