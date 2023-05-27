using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{
    public partial class Logro
    {
        public long Id { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Icono { get; set; }

        public DateTime? Fechadesde { get; set; }
        public DateTime? Fechahasta { get; set; }
    }
}
