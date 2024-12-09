using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class PalletDto
    {
        public long Id { get; set; }
        public long CodigoProducto { get; set; }
        public DateTime Ingreso { get; set; }
        public UbicacionDto? Ubicacion { get; set; }
    }
}
