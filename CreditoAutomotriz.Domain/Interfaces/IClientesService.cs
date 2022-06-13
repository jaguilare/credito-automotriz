using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Entities;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IClientesService
    {

        Task<Respuesta> Agregar(Clientes cliente);
        Task<Respuesta> Actualizar(Clientes cliente);
        Task<Respuesta> Eliminar(string identificacion);
        Task<Respuesta> Consultar(string identificacion);

    }
}
