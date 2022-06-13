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
    [Route("vehiculos")]
    [ApiController]
    public class VehiculosController : ControllerBase
    {
        private readonly ILogger ILogger;

        private readonly IVehiculosService _vehiculosService;

        public VehiculosController(ILogger<ClientesController> iLogger, IVehiculosService vehiculosService)
        {
            this.ILogger = iLogger;
            _vehiculosService = vehiculosService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Agregar(Vehiculos vehiculo)
        {
            Respuesta respuesta = await _vehiculosService.Agregar(vehiculo);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(Vehiculos vehiculo)
        {
            Respuesta respuesta = await _vehiculosService.Actualizar(vehiculo);
            return respuesta;
        }

        [HttpDelete("{placa}")]
        public async Task<ActionResult<Respuesta>> Eliminar(string placa)
        {
            Respuesta respuesta = await _vehiculosService.Eliminar(placa);
            return respuesta;
        }

        [HttpGet]
        public async Task<ActionResult<Respuesta>> Consultar([FromQuery] ConsultarVehiculosDto dto)
        {
            Respuesta respuesta = await _vehiculosService.Consultar(dto);
            return respuesta;
        }

    }
}