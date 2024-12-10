using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class UbicacionDto
    {
        public long Id { get; set; }
        public int Fila {  get; set; }
        public int Columna { get; set; }
    }
}
