using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace _3.Presentation._3.ViewModel
{
    public class TerrariosViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Terrario> _terrarios;
        public ObservableCollection<Terrario> Terrarios
        {
            get { return _terrarios; }
            set
            {
                if (_terrarios != value)
                {
                    _terrarios = value;
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
