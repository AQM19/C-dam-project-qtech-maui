using _4.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _3.Presentation._3.ViewModel
{
    public class TareasViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Tarea> _tareas;
        public ObservableCollection<Tarea> Tareas
        {
            get { return _tareas; }
            set
            {
                if (_tareas != value)
                {
                    _tareas = value;
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
