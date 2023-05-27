using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{
    public partial class UsuarioLogro
    {
        public long Idusuario { get; set; }

        public long Idlogro { get; set; }

        public DateTime FechaAdquisicion { get; set; }
    }
}
