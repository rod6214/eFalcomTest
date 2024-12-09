using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICentroDistribucionService
    {
        Task<Pallet> GetPalletAsync(long id);
        Task<Pallet?> GetPalletByCodigoAsync(long codigoProducto);
        Task<List<Pallet>> GetPalletsAsync();
        Task<Pallet> InsertPalletAsync(Pallet pallet);
        Task DeletePalletAsync(long id);
        Task<Ubicacion> IsertLocationAsync(Ubicacion ubicacion);
        Task UpdateLocationAsync(Ubicacion? ubicacion);
        Task DeleteLocationAsync(long id);
        Task<Movimiento> GetMovimientoAsync(long id);
        Task<List<Movimiento>> GetMovimientosAsync();
        Task CreateMovimientoAsync(Movimiento movimiento);
        Task<Ubicacion> GetUbicacionAsync(long id);
        Task<List<Ubicacion>> GetUbicacionesAsync();
        Task<Ubicacion> GetUbicacionByCodigoAsync(long codigo);
        Task<Ubicacion> GetUbicacionByFilaColumnAsync(int row, int column);
    }
}
