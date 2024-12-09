
using CentroDistribucion.Database.Contexts;
using Domain;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Linq;

namespace CentroDistribucion.Database.Implementations
{
    public class CentroDistribucionRepository : ICentroDistribucionService
    {
        private readonly CentroDistribucionContext context;

        public CentroDistribucionRepository(CentroDistribucionContext context)
        {
            this.context = context;
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
                var pallet = context.Pallets.Find(id);
                if (pallet is not null) 
                {
                    context.Pallets.Remove(pallet);
                    await context.SaveChangesAsync();
                }
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

        public async Task<Pallet> GetPallet(long id)
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

        public async Task<Ubicacion> GetUbicacionByCodigoAsync(long codigo)
        {
            var ub = from ubicacion in context.Ubicaciones
                     join pallet in context.Pallets
                     on ubicacion.Id equals pallet.UbicacionId
                     orderby ubicacion.Columna, ubicacion.Fila
                     where pallet.Id == codigo
                     select ubicacion;
            var result = await ub.ToListAsync();
            return result.First();
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

        public async Task InsertPalletAsync(Pallet pallet)
        {
            try
            {
                context.Pallets.Add(pallet);
                await context.SaveChangesAsync();
            }
            catch 
            {
                throw;
            }
        }

        public async Task IsertLocationAsync(Ubicacion ubicacion)
        {
            try
            {
                context.Ubicaciones.Add(ubicacion);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateLocationAsync(Ubicacion? ubicacion)
        {
            try
            {
                if (ubicacion == null) return;
                var ub = await context.Ubicaciones.FindAsync(ubicacion.Id);
                if (ub != null)
                    context.Update(ub);
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
