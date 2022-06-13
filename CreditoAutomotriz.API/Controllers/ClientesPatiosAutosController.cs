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
    [Route("clientes-patios-autos")]
    [ApiController]
    public class ClientesPatiosAutosController : ControllerBase
    {
        private readonly ILogger ILogger;

        private readonly IClientesPatiosAutosService _clientesPatiosAutosService;

        public ClientesPatiosAutosController(
            ILogger<ClientesController> iLogger,
            IClientesPatiosAutosService clientesPatiosAutosService)
        {
            this.ILogger = iLogger;
            _clientesPatiosAutosService = clientesPatiosAutosService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Agregar(ClientesPatiosAutos vehiculo)
        {
            Respuesta respuesta = await _clientesPatiosAutosService.Agregar(vehiculo);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(ClientesPatiosAutos vehiculo)
        {
            Respuesta respuesta = await _clientesPatiosAutosService.Actualizar(vehiculo);
            return respuesta;
        }

        [HttpDelete("{identificacionCliente}")]
        public async Task<ActionResult<Respuesta>> Eliminar(string identificacionCliente)
        {
            Respuesta respuesta = await _clientesPatiosAutosService.Eliminar(identificacionCliente);
            return respuesta;
        }

        [HttpGet("{identificacionCliente}")]
        public async Task<ActionResult<Respuesta>> Consultar(string identificacionCliente)
        {
            Respuesta respuesta = await _clientesPatiosAutosService.Consultar(identificacionCliente);
            return respuesta;
        }

    }
}