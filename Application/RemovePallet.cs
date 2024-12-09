using Application.Options;
using AutoMapper;
using Domain;
using Domain.Enums;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class RemovePallet : IRequest
    {
        [Required]
        public long CodigoProducto { get; set; }
    }

    public class RemovePalletHandler : IRequestHandler<RemovePallet>
    {
        private readonly ICentroDistribucionService centroDistribucion;
        private readonly IMapper mapper;
        private readonly int maxPalletsByLoc;

        public RemovePalletHandler(ICentroDistribucionService centroDistribucion, IMapper mapper, IOptions<ApplicationOptions> options)
        {
            this.centroDistribucion = centroDistribucion;
            this.mapper = mapper;
            maxPalletsByLoc = options.Value.MaxPalletsByLoc;
        }

        public async Task Handle(RemovePallet request, CancellationToken cancellationToken)
        {
            try
            {
                // Necesitamos la primera paleta disponible o la primera que fue ingresada
                var pallet = await centroDistribucion.GetPalletByCodigoAsync(request.CodigoProducto);
                
                if (pallet is not null) 
                {
                    await centroDistribucion.UpdateLocationAsync(new Ubicacion 
                    {
                        Id = pallet.UbicacionId,
                        Ocupado = pallet.Ubicacion?.Pallets?.Count - 1 > maxPalletsByLoc,
                    });

                    await centroDistribucion.DeletePalletAsync(request.CodigoProducto);
                    await centroDistribucion.CreateMovimientoAsync(new Movimiento 
                    {
                        PalletId = pallet.Id,
                        Fecha = DateTime.Now,
                        Type = (int)TipoMovimiento.EGRESO
                    });
                }
            }
            catch 
            {
                throw;
            }
        }
    }
}
