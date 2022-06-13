using CreditoAutomotriz.Infrastructure.AppContextDB;
using CreditoAutomotriz.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditoAutomotriz.Repository.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _dbSet;
        private readonly CreditoAutomotrizContext _dbContext;

        public GenericRepository(CreditoAutomotrizContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Actualizar(TEntity obj)
        {
            _dbSet.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }

        public async Task<TEntity> Agregar(TEntity obj)
        {
            var set = await _dbSet.AddAsync(obj);
            return set.Entity;
        }

        public async Task Agregar(IEnumerable<TEntity> obj)
        {
            await _dbSet.AddRangeAsync(obj);
        }

        public void Eliminar(TEntity obj)
        {
            _dbSet.Remove(obj);
        }

        public async Task<int> Guardar()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Consultar()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> Consultar(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> ConsultarRelacionado(string claseRelacion, System.Linq.Expressions.Expression<Func<TEntity, bool>> predicado)
        {
            return await _dbContext.Set<TEntity>().Include(claseRelacion).FirstOrDefaultAsync(predicado);
        }

        public async Task<TEntity> ConsultarFiltro(Func<TEntity, bool> predicado)
        {
            return await Task.FromResult(_dbSet.Where(predicado).FirstOrDefault());
        }

        public async Task<IEnumerable<TEntity>> ConsultarListaFiltro(Func<TEntity, bool> predicado)
        {
            return await Task.FromResult(_dbSet.Where(predicado).ToList());
        }

    }
}
