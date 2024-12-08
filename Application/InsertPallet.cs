using AutoMapper;
using Domain;
using Domain.Dtos;
using Domain.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class InsertPallet : IRequest<bool>
    {
        [Required]
        public long CodigoProducto { get; set; }
    }

    public class InsertPalletHandler : IRequestHandler<InsertPallet, bool>
    {
        private readonly ICentroDistribucionService centroDistribucion;
        private readonly IMapper mapper;

        public InsertPalletHandler(ICentroDistribucionService centroDistribucion, IMapper mapper) 
        {
            this.centroDistribucion = centroDistribucion;
            this.mapper = mapper;
        }

        public async Task<bool> Handle(InsertPallet request, CancellationToken cancellationToken)
        {
            try
            {
                var pallet = mapper.Map<Pallet>(request);
                await centroDistribucion.InsertPalletAsync(pallet);
                return true;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
