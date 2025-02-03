namespace DomainLayer.partonair.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        Task<ICollection<T>> GetAllAsync();
    }
}
