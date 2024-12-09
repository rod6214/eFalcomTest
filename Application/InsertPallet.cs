using Application.Options;
using AutoMapper;
using Domain;
using Domain.Enums;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.Options;

using System.ComponentModel.DataAnnotations;
using System.Data.Common;


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
        private readonly int maxPalletsByLoc;

        public InsertPalletHandler(ICentroDistribucionService centroDistribucion, IMapper mapper, IOptions<ApplicationOptions> options) 
        {
            this.centroDistribucion = centroDistribucion;
            this.mapper = mapper;
            maxPalletsByLoc = options.Value.MaxPalletsByLoc;
        }

        public async Task<bool> Handle(InsertPallet request, CancellationToken cancellationToken)
        {
            try
            {
                // Intentamos crear una ubicacion valida, si ya existe intentara actualizar el campo que determina
                // si la ubicacion sigue libre
                var relativePos = await tryToCreateUbicacion(request);

                var pallet = mapper.Map<Pallet>(request);

                pallet.UbicacionId = relativePos.Id;

                pallet = await centroDistribucion.InsertPalletAsync(pallet);
                await centroDistribucion.CreateMovimientoAsync(new Movimiento
                {
                    PalletId = pallet.Id,
                    Fecha = DateTime.Now,
                    Type = (int)TipoMovimiento.INGRESO
                });
                return true;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        private async Task<Ubicacion> tryToCreateUbicacion(InsertPallet request)
        {
            //vamos a tratar de crear una ubicacion si no existe ubicacion para cierto codigo de producto
            // tambien debemos crear una ubicacion si ya existe, pero esta ocupado
            Ubicacion? ubicacion = new Ubicacion();

            ubicacion = await centroDistribucion.GetUbicacionByCodigoAsync(request.CodigoProducto);

            var tuple = await getNextPosition();
            if (ubicacion is null)
            {
                ubicacion = new Ubicacion
                {
                    Fila = tuple.Item1,
                    Columna = tuple.Item2,
                };
                ubicacion = await centroDistribucion.IsertLocationAsync(ubicacion);
                return ubicacion;
            }
            else
            {
                if (ubicacion.Pallets?.Count == maxPalletsByLoc)
                {
                    // Se cierra el apilado actual
                    await centroDistribucion.UpdateLocationAsync(new Ubicacion
                    {
                        Id = ubicacion.Id,
                        Ocupado = true
                    });

                    // Se crea nueva ubicacion para agrupar pallets
                    ubicacion = await centroDistribucion.IsertLocationAsync(new Ubicacion
                    {
                        Fila = tuple.Item1,
                        Columna = tuple.Item2,
                    });
                }
            }

            if (ubicacion is null) throw new Exception("Invalid operation");

            return ubicacion;
        }

        private async Task<Tuple<int, int>> getNextPosition() 
        {
            // Empezamos incrementando las columnas cuando se desborde, se aumenta la fila
            var ubicaciones = await centroDistribucion.GetUbicacionesAsync();
            // Si no hay ubicaciones, no hay ubicaciones registradas
            var lastLocation = ubicaciones.OrderBy(x => x.Id).LastOrDefault();
            
            if (lastLocation != null) 
            {
                var newColumn = lastLocation.Columna+1;
                var newRow = lastLocation.Fila;
                if (newColumn > maxPalletsByLoc) 
                {
                    newColumn = 0;
                    newRow++;
                }

                return Tuple.Create(newRow, newColumn);
            }

            return new Tuple<int, int>(0, 0);
        }
    }
}
