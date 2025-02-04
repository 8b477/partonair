using DomainLayer.partonair.Contracts;

using InfrastructureLayer.partonair.Exceptions;
using InfrastructureLayer.partonair.Exceptions.Enums;
using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.partonair.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        protected GenericRepository(AppDbContext ctx)
        {
            _ctx = ctx 
                ?? throw new InfrastructureLayerException(InfrastructureLayerErrorType.DatabaseConnectionError, $"- Context name : {nameof(_ctx)}");

            _dbSet = _ctx.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
           var result = await _dbSet.AddAsync(entity);
           return result.Entity;
        }

        public virtual void DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

    }
}
