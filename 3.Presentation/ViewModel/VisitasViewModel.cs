using _4.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _3.Presentation.ViewModel
{
    public class VisitasViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Visita> _visitas;
        public ObservableCollection<Visita> Visitas
        {
            get { return _visitas; }
            set
            {
                if (_visitas != value)
                {
                    _visitas = value;
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
