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
    public class UbicacionConfiguration : IEntityTypeConfiguration<Ubicacion>
    {
        public void Configure(EntityTypeBuilder<Ubicacion> builder)
        {
            builder.ToTable("Ubicaciones", "dbo");
            builder.HasOne(u => u.Pallet)
                .WithMany(p => p.Ubicaciones)
                .HasForeignKey(u => u.PalletId);
        }
    }
}
