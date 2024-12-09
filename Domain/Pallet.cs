using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [Index(nameof(CodigoProducto))]
    public class Pallet
    {
        public long Id { get; set; }
        public long CodigoProducto { get; set; }
        public long UbicacionId { get; set; }
        public bool Removed { get; set; }
        public virtual Ubicacion? Ubicacion { get; set; }
        public virtual List<Movimiento>? Movimientos { get; set; }
        //public virtual List<Ubicacion>? Ubicaciones { get; set; }
        //public required virtual Ubicacion Ubicacion { get; set; }
    }
}
