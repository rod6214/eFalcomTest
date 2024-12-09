using Application.Options;
using AutoMapper;
using Domain.Dtos;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class GetPallets : IRequest<List<PalletDto>>
    {
        public long? CodigoProducto { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }

    public class GetPalletsHandler : IRequestHandler<GetPallets, List<PalletDto>>
    {
        private readonly ICentroDistribucionService centroDistribucion;
        private readonly IMapper mapper;
        private readonly int maxPalletsByLoc;

        public GetPalletsHandler(ICentroDistribucionService centroDistribucion, IMapper mapper, IOptions<ApplicationOptions> options)
        {
            this.centroDistribucion = centroDistribucion;
            this.mapper = mapper;
            maxPalletsByLoc = options.Value.MaxPalletsByLoc;
        }

        public async Task<List<PalletDto>> Handle(GetPallets request, CancellationToken cancellationToken)
        {
            try 
            {
                var pallets = await centroDistribucion.GetPalletsAsync(request.CodigoProducto, 
                    request.FechaDesde, request.FechaHasta);

                if (pallets != null) 
                {
                    var result = mapper.Map<List<PalletDto>>(pallets);
                    return result;
                }

                return new List<PalletDto>();
            }
            catch 
            {
                throw;
            }
        }
    }
}
