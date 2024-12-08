using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class InsertPalletDto
    {
        public long CodigoProducto { get; set; }
        public DateTime Fecha { get; set; }
    }
}
