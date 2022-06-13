using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Dto.Vehiculos;
using CreditoAutomotriz.Entities.Entities;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IVehiculosService
    {

        Task<Respuesta> Agregar(Vehiculos cliente);
        Task<Respuesta> Actualizar(Vehiculos cliente);
        Task<Respuesta> Eliminar(string placa);
        Task<Respuesta> Consultar(ConsultarVehiculosDto dto);

    }
}
