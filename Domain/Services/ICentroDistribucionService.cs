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
        Task InsertPalletAsync(Pallet pallet);
        Task DeletePalletAsync(long id);
    }
}
