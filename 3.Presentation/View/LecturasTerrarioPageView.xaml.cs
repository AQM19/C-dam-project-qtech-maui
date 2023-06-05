using _2.BusinessLogic;
using _3.Presentation.Data;
using _4.Entities;

namespace _3.Presentation.View;

public partial class LecturasTerrarioPageView : ContentPage
{
    private Terrario _terrario;
    public LecturasTerrarioPageView()
    {
        InitializeComponent();
    }

    public LecturasTerrarioPageView(Terrario terrario) : this()
    {
        _terrario = terrario;
        CargarInfo();
    }

    private async void CargarInfo()
    {
        List<Lectura> lecturas = await Herramientas.GetLecturasTerrario(_terrario.Id);
        GraphicsDrawable drawable = new GraphicsDrawable { Lecturas = lecturas };
        graphView.Drawable = drawable;
    }
}