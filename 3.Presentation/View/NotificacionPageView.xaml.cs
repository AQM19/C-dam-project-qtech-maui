using _2.BusinessLogic;
using _3.Presentation._3.ViewModel;
using _4.Entities;
using System.Collections.ObjectModel;

namespace _3.Presentation;

public partial class NotificacionPage : ContentPage
{
    private readonly Usuario _usuario;
    private NotificacionesViewModel ViewModel { get; set; }

    public NotificacionPage()
	{
		InitializeComponent();

        _usuario = App.Usuario;
        this.ViewModel = new NotificacionesViewModel();
        this.BindingContext = ViewModel;

        NotificationPollingComponent.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(NotificationPollingComponent.PendingNotifications))
            {
                CargarNotificaciones();
            }
        };

        CargarNotificaciones();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarNotificaciones();
    }

    private async void CargarNotificaciones()
    {
        this.ViewModel.Notificaciones = this.ViewModel.Notificaciones ?? new ObservableCollection<Notificacion>();
        this.ViewModel.Notificaciones.Clear();

        List<Notificacion> notificacions = await Herramientas.GetNotificacionesByUserId(_usuario.Id);
        ObservableCollection<Notificacion> observables = new ObservableCollection<Notificacion>(notificacions);
        this.ViewModel.Notificaciones = observables;
    }

    private async void LvNotificaciones_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Notificacion noti = (Notificacion)LvNotificaciones.SelectedItem;
        noti.Vista = 1;

        await Herramientas.UpdateNotificacion(noti, noti.Id);
        CargarNotificaciones();
    }
}