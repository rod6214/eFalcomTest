using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Movimiento
    {
        public long Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Type { get; set; }
        public virtual long UbicacionId { get; set; }
        public required virtual Ubicacion Ubicacion { get; set; }
    }
}
