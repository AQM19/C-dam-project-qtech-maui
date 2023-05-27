using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{
    public partial class Observacion
    {
        public long Id { get; set; }

        public long Idterrario { get; set; }

        public DateTime Fecha { get; set; }

        public string Texto { get; set; }
    }
}
