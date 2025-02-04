using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;


namespace ApplicationLayer.partonair.Interfaces
{
    public interface IUserService
    {
        // Commands
        Task<UserViewDTO> CreateAsync(UserCreateDTO entity);
        void Update(UserUpdateDTO entity);
        void Delete(Guid id);

        // Queries
        Task<ICollection<UserViewDTO>> GetAllAsync();
        Task<UserViewDTO> GetByIdAsyncService(Guid id);
        Task<ICollection<User>> GetByNameAsyncService(string name);
        Task<User> GetByEmailAsyncService(string email);
        Task<ICollection<User>> GetByRoleAsyncService(string role);
        Task<bool> IsEmailAvailableService(string email);
    }
}
