using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation;

public partial class NotificacionPage : ContentPage
{
    private readonly Usuario _usuario;
    private NotificationViewModel ViewModel { get; set; }

    public NotificacionPage()
	{
		InitializeComponent();

        _usuario = App.Usuario;
        this.ViewModel = new NotificationViewModel();
        this.BindingContext = ViewModel;
        CargarNotificaciones();
    }

    private async void CargarNotificaciones()
    {
        this.ViewModel.Notificaciones = this.ViewModel.Notificaciones ?? new ObservableCollection<Notificacion>();
        this.ViewModel.Notificaciones.Clear();

        List<Notificacion> notificacions = await Herramientas.GetNotificacionesByUserId(_usuario.Id);

        if (notificacions.Count > 0)
        {
            foreach (Notificacion n in notificacions)
            {
                this.ViewModel.Notificaciones.Add(n);
            };
        };
    }
}