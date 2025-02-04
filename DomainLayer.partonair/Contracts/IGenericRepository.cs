namespace DomainLayer.partonair.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        T Update(T entity);
        void Delete(T entity);
        Task<ICollection<T>> GetAllAsync();
    }
}
