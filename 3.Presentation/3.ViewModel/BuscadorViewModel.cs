using _2.BusinessLogic;
using _4.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace _3.Presentation._3.ViewModel
{
    public class BuscadorViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Usuario> _usuarios;
        public ObservableCollection<Usuario> Usuarios
        {
            get { return _usuarios; }
            set
            {
                if (_usuarios != value)
                {
                    _usuarios = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            List<Usuario> result = await Herramientas.Search(query);

            if (result.Count > 0)
            {
                if(Usuarios.Count> 0)
                {
                    Usuarios.Clear();
                }

                foreach(Usuario u in result)
                {
                    Usuarios.Add(u);
                }

            }
        });
    }
}
