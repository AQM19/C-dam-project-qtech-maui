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


    public class DashboardViewModel : INotifyPropertyChanged
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
