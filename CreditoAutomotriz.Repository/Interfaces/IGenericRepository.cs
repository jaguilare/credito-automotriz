using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository.Interfaces
{
    public interface IGenericRepository<TEntity>
    {

        Task<IEnumerable<TEntity>> Consultar();
        Task<TEntity> Consultar(int id);
        Task<TEntity> ConsultarRelacionado(string claseRelacion, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicado);
        Task<TEntity> ConsultarFiltro(Func<TEntity, bool> predicado);
        Task<IEnumerable<TEntity>> ConsultarListaFiltro(Func<TEntity, bool> predicado);
        Task<TEntity> Agregar(TEntity obj);
        Task Agregar(IEnumerable<TEntity> obj);
        void Eliminar(TEntity obj);
        void Actualizar(TEntity obj);

        Task<int> Guardar();


    }
}
