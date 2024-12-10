
using CentroDistribucion.Database.Contexts;
using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Linq;
using Domain.Enums;

namespace CentroDistribucion.Database.Implementations
{
    public class CentroDistribucionRepository : ICentroDistribucionService
    {
        private readonly CentroDistribucionContext context;

        public CentroDistribucionRepository(CentroDistribucionContext context)
        {
            this.context = context;
        }

        public async Task<Movimiento> CreateMovimientoAsync(Movimiento movimiento)
        {
            try
            {
                if (movimiento == null) throw new Exception("Invalid input");
                await context.AddAsync(movimiento);
                await context.SaveChangesAsync();

                var insertedMovimiento = await (from m in context.Movimientos
                                            where m.PalletId == movimiento.PalletId
                                            select m).ToListAsync();
                return insertedMovimiento.First();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteLocationAsync(long id)
        {
            try
            {
                var ubicacion = context.Ubicaciones.Find(id);
                if (ubicacion is not null)
                {
                    context.Ubicaciones.Remove(ubicacion);
                    await context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeletePalletAsync(long id)
        {
            try
            {
                var pallet = await (from pa in context.Pallets
                             where pa.Id == id
                             select pa).FirstOrDefaultAsync();

                if (pallet is not null)
                {
                    pallet.Removed = true;
                    context.Update(pallet);
                    await context.SaveChangesAsync();
                }
                else
                    throw new Exception("Product pallet is not available.");
            }
            catch 
            {
                throw;
            }
        }

        public async Task<Pallet?> GetLastPallet()
        {
            try
            {
                var last = await (from p in context.Pallets
                           orderby p.Id descending
                           select p).FirstOrDefaultAsync();
                return last;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<Movimiento> GetMovimientoAsync(long id)
        {
            try
            {
                var result = await context.Movimientos.FindAsync(id);
                if (result is not null)
                    return result;
                else
                    throw new Exception("Record not found");
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Movimiento>> GetMovimientosAsync()
        {
            try
            {
                var result = await context.Movimientos.ToListAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Pallet> GetPalletAsync(long id)
        {
            try
            {
                var result = await context.Pallets.FindAsync(id);
                if (result is not null)
                    return result;
                else
                    throw new Exception("Record not found");
            }
            catch 
            {
                throw;
            }
        }

        public async Task<Pallet?> GetPalletByCodigoAsync(long codigoProducto)
        {
            try
            {
                var firtsPallet = await (from pallet in context.Pallets
                                  join ubicacion in context.Ubicaciones
                                  on pallet.UbicacionId equals ubicacion.Id
                                  where pallet.CodigoProducto == codigoProducto && !pallet.Removed
                                  orderby pallet.Id ascending
                                  select pallet).ToListAsync();
                return firtsPallet.FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Pallet>> GetPalletsAsync()
        {
            try
            {
                var result = await context.Pallets.ToListAsync();
                return result;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<List<Pallet>> GetPalletsAsync(long? codigoProducto, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            try 
            {
                var pallets = await (from p in context.Pallets
                                     join m in context.Movimientos
                                     on p.Id equals m.PalletId
                                     where !p.Removed  
                                     && (!codigoProducto.HasValue || p.CodigoProducto == codigoProducto)
                                     && (!fechaDesde.HasValue || m.Fecha >= fechaDesde && (m.Type == (int)TipoMovimiento.INGRESO))
                                     && (!fechaHasta.HasValue || m.Fecha <= fechaHasta && (m.Type == (int)TipoMovimiento.INGRESO))
                                     orderby p.UbicacionId
                                     select p).ToListAsync();
                return pallets;

            }
            catch 
            {
                throw;
            }
        }

        public async Task<Ubicacion> GetUbicacionAsync(long id)
        {
            try
            {
                var result = await context.Ubicaciones.FindAsync(id);
                if (result is not null)
                    return result;
                else
                    throw new Exception("Record not found");
            }
            catch
            {
                throw;
            }
        }

        public async Task<Ubicacion?> GetUbicacionByCodigoAsync(long codigo)
        {
            var ub = from ubicacion in context.Ubicaciones
                     join pallet in context.Pallets
                     on ubicacion.Id equals pallet.UbicacionId
                     orderby ubicacion.Id ascending
                     where pallet.CodigoProducto == codigo && !ubicacion.Ocupado
                     select ubicacion;
            var result = await ub.FirstOrDefaultAsync();
            return result;
        }

        public async Task<Ubicacion> GetUbicacionByFilaColumnAsync(int row, int column)
        {
            var ub = await (from ubicacion in context.Ubicaciones
                     where ubicacion.Fila == row && ubicacion.Columna == column
                     select ubicacion).ToListAsync();
            return ub.First();
        }

        public async Task<List<Ubicacion>> GetUbicacionesAsync()
        {
            try
            {
                var result = await context.Ubicaciones.ToListAsync();
                return result;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Pallet> InsertPalletAsync(Pallet pallet)
        {
            try
            {
                context.Pallets.Add(pallet);
                await context.SaveChangesAsync();
                var insertedPallet = await (from p in context.Pallets
                                    where p.CodigoProducto == pallet.CodigoProducto
                                    orderby p.Id
                                    select p).ToListAsync();
                return insertedPallet.Last();
            }
            catch 
            {
                throw new Exception("Invalid operation");
            }
        }

        public async Task<Ubicacion> IsertLocationAsync(Ubicacion ubicacion)
        {
            try
            {
                context.Ubicaciones.Add(ubicacion);
                await context.SaveChangesAsync();
                var insertedUbicacion = await (from p in context.Ubicaciones
                                            where p.Fila == ubicacion.Fila && p.Columna == ubicacion.Columna
                                            select p).ToListAsync();
                return insertedUbicacion.First();
            }
            catch
            {
                throw new Exception("Invalid operation");
            }
        }

        public async Task UpdateLocationAsync(Ubicacion? ubicacion)
        {
            try
            {
                if (ubicacion == null) return;
                var ub = await context.Ubicaciones.FindAsync(ubicacion.Id);
                if (ub != null)
                {
                    ub.Ocupado = ubicacion.Ocupado;
                    context.Update(ub);
                }
                else
                    throw new Exception("Record not found");
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
