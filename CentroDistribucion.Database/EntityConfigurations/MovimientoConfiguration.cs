using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroDistribucion.Database.EntityConfigurations
{
    public class MovimientoConfiguration : IEntityTypeConfiguration<Movimiento>
    {
        public void Configure(EntityTypeBuilder<Movimiento> builder)
        {
            builder.ToTable("Movimientos", "dbo");
            builder.HasOne(m => m.Ubicacion)
                .WithMany(u => u.Movimientos)
                .HasForeignKey(m => m.UbicacionId);
        }
    }
}
