using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{
    public partial class Tarea
    {
        public long Id { get; set; }

        public long Idterrario { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public DateTime? FechaResolucion { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string Estado { get; set; }

    }
}
