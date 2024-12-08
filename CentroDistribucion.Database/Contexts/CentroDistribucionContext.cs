using CentroDistribucion.Database.Options;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroDistribucion.Database.Contexts
{
    public class CentroDistribucionContext : DbContext
    {
        private string config;
        public virtual DbSet<Pallet> Pallets { get; set; }
        public virtual DbSet<Ubicacion> Ubicaciones { get; set; }
        public virtual DbSet<Movimiento> Movimientos { get; set; }

        public CentroDistribucionContext(DbContextOptions<CentroDistribucionContext> optionsBuilder, IOptions<CentroDistribucionOption> configOption) 
            : base (optionsBuilder)
        {
            config = configOption.Value.LocalConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlServer(config);
            }
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CentroDistribucionContext).Assembly);
        }
    }
}
