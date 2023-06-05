using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3.Presentation._3.ViewModel
{
    public class EspeciesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Especie> _especies;
        public ObservableCollection<Especie> Especies
        {
            get { return _especies; }
            set
            {
                if (_especies != value)
                {
                    _especies = value;
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
