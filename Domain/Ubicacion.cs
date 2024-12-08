using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Ubicacion
    {
        public long Id { get; set; }
        public int Fila {  get; set; }
        public int Columna { get; set; }
        public bool Ocupado { get; set; }
        public virtual long PalletId { get; set; }
        public required virtual Pallet Pallet { get; set; }
        public required virtual List<Movimiento> Movimientos { get; set; }
    }
}
