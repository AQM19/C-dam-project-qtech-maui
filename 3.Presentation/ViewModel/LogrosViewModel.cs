using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3.Presentation._3.ViewModel
{
    public class LogrosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Logro> _logros;
        public ObservableCollection<Logro> Logros
        {
            get { return _logros; }
            set
            {
                if (_logros != value)
                {
                    _logros = value;
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
