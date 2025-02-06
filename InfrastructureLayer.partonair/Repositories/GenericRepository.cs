using DomainLayer.partonair.Contracts;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;
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

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual T Update(T entity)
        {
            var result = _dbSet.Update(entity);

            return result.Entity;
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

    }
}
