using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4.Entities
{
    public partial class EspecieTerrario
    {
        public long Idterrario { get; set; }

        public long Idespecie { get; set; }

        public DateTime? FechaInsercion { get; set; }
    }
}
