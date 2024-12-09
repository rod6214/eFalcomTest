using Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CentroDistribucionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalletController : ControllerBase
    {
        private readonly IMediator mediator;
        public PalletController(IMediator mediator) 
        {
            this.mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePallet([FromBody] InsertPallet pallet)
        {
            try 
            {
                await mediator.Send(pallet);
                return Ok();
            }
            catch 
            {
                return BadRequest();
                //throw;
            }
        }

        [HttpDelete("extract")]
        public async Task<ActionResult> RemovePallet([FromQuery] long codigo)
        {
            try
            {
                await mediator.Send(new RemovePallet { CodigoProducto = codigo });
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("pallet")]
        public async Task<ActionResult> GetPallets([FromQuery] long? codigo, DateTime? desde, DateTime? hasta) 
        {
            try 
            {
                var pallets = await mediator.Send(new GetPallets 
                {
                    CodigoProducto = codigo,
                    FechaDesde = desde,
                    FechaHasta = hasta
                });
                return Ok(pallets);
            }
            catch 
            {
                return BadRequest();
            }
        }
    }
}
