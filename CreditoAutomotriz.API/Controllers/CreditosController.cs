using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Dto.Vehiculos;
using CreditoAutomotriz.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    //[ApiVersion("1")]
    // [Route(Constants.RouteVersion)]
    [Route("creditos")]
    [ApiController]
    public class CreditosController : ControllerBase
    {
        private readonly ILogger ILogger;

        private readonly ICreditosService _creditosService;

        public CreditosController(ILogger<ClientesController> iLogger, ICreditosService creditosService)
        {
            this.ILogger = iLogger;
            _creditosService = creditosService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Agregar(SolicitudCredito credito)
        {
            Respuesta respuesta = await _creditosService.Agregar(credito);
            return respuesta;
        }


    }
}