using CreditoAutomotriz.Domain.Interfaces;
using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CreditoAutomotriz.API.Controllers
{
    //[ApiVersion("1")]
    // [Route(Constants.RouteVersion)]
    [Route("clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ILogger ILogger;

        private readonly IClientesService _clientesService;

        public ClientesController(ILogger<ClientesController> iLogger, IClientesService clientesService)
        {
            this.ILogger = iLogger;
            _clientesService = clientesService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Agregar(Clientes cliente)
        {
            Respuesta respuesta = await _clientesService.Agregar(cliente);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(Clientes cliente)
        {
            Respuesta respuesta = await _clientesService.Actualizar(cliente);
            return respuesta;
        }

        [HttpDelete("{identificacion}")]
        public async Task<ActionResult<Respuesta>> Eliminar(string identificacion)
        {
            Respuesta respuesta = await _clientesService.Eliminar(identificacion);
            return respuesta;
        }

        [HttpGet("{identificacion}")]
        public async Task<ActionResult<Respuesta>> Consultar(string identificacion)
        {
            Respuesta respuesta = await _clientesService.Consultar(identificacion);
            return respuesta;
        }

    }
}