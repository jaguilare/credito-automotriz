using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Dto.Vehiculos;
using CreditoAutomotriz.Entities.Entities;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IClientesPatiosAutosService
    {

        Task<Respuesta> Agregar(ClientesPatiosAutos clientePatio);
        Task<Respuesta> Actualizar(ClientesPatiosAutos clientePatio);
        Task<Respuesta> Eliminar(string identificacionCliente);
        Task<Respuesta> Consultar(string identificacionCliente);

    }
}
