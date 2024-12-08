using Microsoft.EntityFrameworkCore;

namespace Domain
{
    [Index(nameof(CodigoProducto))]
    public class Pallet
    {
        public long Id { get; set; }
        public long CodigoProducto { get; set; }
        public virtual List<Ubicacion>? Ubicaciones { get; set; }
    }
}
