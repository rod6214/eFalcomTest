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
    public class PalletConfiguration : IEntityTypeConfiguration<Pallet>
    {
        public void Configure(EntityTypeBuilder<Pallet> builder)
        {
            builder.ToTable("Pallets", "dbo");
            builder.Property(p => p.CodigoProducto).IsRequired();
            builder.HasMany(p => p.Movimientos)
                .WithOne(m => m.Pallet)
                .HasForeignKey(p => p.PalletId);
        }
    }
}
