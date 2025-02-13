namespace DomainLayer.partonair.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> Update(T entity);
        Task Delete(Guid id);
        Task<T> GetByGuidAsync(Guid id);
        Task<ICollection<T>> GetAllAsync();
    }
}
