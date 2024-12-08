
using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentroDistribucion.Database.Implementations
{
    public class CentroDistribucionRepository : ICentroDistribucionService
    {
        public Task DeletePalletAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task InsertPalletAsync(Pallet pallet)
        {
            throw new NotImplementedException();
        }
    }
}
