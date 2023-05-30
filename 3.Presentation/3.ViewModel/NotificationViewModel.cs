using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3.Presentation._3.ViewModel
{
    public class NotificationViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Notificacion> _notificaciones;
        public ObservableCollection<Notificacion> Notificaciones
        {
            get { return _notificaciones; }
            set
            {
                if(_notificaciones != value)
                {
                    _notificaciones = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
