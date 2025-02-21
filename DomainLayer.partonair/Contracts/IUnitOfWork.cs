
namespace DomainLayer.partonair.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        // Repo
        IUserRepository Users { get; }
        IProfileRepository Profiles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
