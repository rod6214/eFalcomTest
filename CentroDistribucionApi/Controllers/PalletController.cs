using Application;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult> Pallet([FromBody] InsertPallet pallet)
        {
            await mediator.Send(pallet);
            return Ok();
        }
    }
}
