using CreditoAutomotriz.Entities.App;
using CreditoAutomotriz.Entities.Dto.Vehiculos;
using CreditoAutomotriz.Entities.Entities;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Domain.Interfaces
{
    public interface IPatiosAutosService
    {

        Task<Respuesta> Agregar(PatiosAutos patioAutos);
        Task<Respuesta> Actualizar(PatiosAutos patioAutos);
        Task<Respuesta> Eliminar(int idPatiosAutos);
        Task<Respuesta> Consultar(string nombre);

    }
}
