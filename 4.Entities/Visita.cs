using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{
    public partial class Visita
    {
        public long Id { get; set; }

        public long Idusuario { get; set; }

        public long Idterrario { get; set; }

        public DateTime Fecha { get; set; }

        public string Comentario { get; set; }

        public float Puntuacion { get; set; }
    }
}
