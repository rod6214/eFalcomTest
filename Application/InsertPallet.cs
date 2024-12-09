using Application.Options;
using AutoMapper;
using Domain;
using Domain.Dtos;
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
                // Intentamos crear una ubicacion valida
                var relativePos = tryToCreateUbicacion(request);

                var pallet = mapper.Map<Pallet>(request);

                pallet.UbicacionId = relativePos.Id;

                await centroDistribucion.InsertPalletAsync(pallet);
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
            Ubicacion ubicacion = new Ubicacion();

            ubicacion = await centroDistribucion.GetUbicacionByCodigoAsync(request.CodigoProducto);


            if (ubicacion is null || ubicacion.Ocupado)
            {
                var tuple = await getNextPosition();
                ubicacion = new Ubicacion
                {
                    Fila = tuple.Item1,
                    Columna = tuple.Item2,
                    Ocupado = false
                };
                await centroDistribucion.IsertLocationAsync(ubicacion);
                ubicacion = await centroDistribucion.GetUbicacionByFilaColumnAsync(ubicacion.Fila, ubicacion.Columna);
                return ubicacion;
            }

            if (ubicacion is not null && ubicacion.Pallets != null)
            {
                // Actualiza la ubicacion y calcula si ya esta ocupado
                await centroDistribucion.UpdateLocationAsync(new Ubicacion 
                {
                    Id = ubicacion.Id,
                    Columna = ubicacion.Columna,
                    Fila = ubicacion.Fila,
                    Ocupado = ubicacion.Pallets.Count > maxPalletsByLoc
                });
                ubicacion = await centroDistribucion.GetUbicacionByFilaColumnAsync(ubicacion.Fila, ubicacion.Columna);
                return ubicacion;
            }



            if (ubicacion is null) throw new Exception("Invalid operation");

            return ubicacion;
        }

        private async Task<Tuple<int, int>> getNextPosition() 
        {
            // Empezamos incrementando las columnas cuando se desborde, se aumenta la fila
            var ubicaciones = await centroDistribucion.GetUbicacionesAsync();
            // Si no hay ubicaciones, no hay ubicaciones registradas
            var lastLocation = ubicaciones.LastOrDefault();
            
            if (lastLocation != null) 
            {
                var newColumn = lastLocation.Columna++;
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
