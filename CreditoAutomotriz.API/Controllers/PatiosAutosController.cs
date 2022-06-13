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
    [Route("patios-autos")]
    [ApiController]
    public class PatiosAutosController : ControllerBase
    {
        private readonly ILogger ILogger;

        private readonly IPatiosAutosService _patiosAutosService;

        public PatiosAutosController(ILogger<ClientesController> iLogger, IPatiosAutosService patiosAutosService)
        {
            this.ILogger = iLogger;
            _patiosAutosService = patiosAutosService;
        }

        [HttpPost]
        public async Task<ActionResult<Respuesta>> Agregar(PatiosAutos patioAutos)
        {
            Respuesta respuesta = await _patiosAutosService.Agregar(patioAutos);
            return respuesta;
        }

        [HttpPut]
        public async Task<ActionResult<Respuesta>> Actualizar(PatiosAutos patioAutos)
        {
            Respuesta respuesta = await _patiosAutosService.Actualizar(patioAutos);
            return respuesta;
        }

        [HttpDelete("{idPatioAutos}")]
        public async Task<ActionResult<Respuesta>> Eliminar(int idPatioAutos)
        {
            Respuesta respuesta = await _patiosAutosService.Eliminar(idPatioAutos);
            return respuesta;
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Respuesta>> Consultar(string nombre)
        {
            Respuesta respuesta = await _patiosAutosService.Consultar(nombre);
            return respuesta;
        }

    }
}