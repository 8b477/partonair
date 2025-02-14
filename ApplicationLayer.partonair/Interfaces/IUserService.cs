using ApplicationLayer.partonair.DTOs;


namespace ApplicationLayer.partonair.Interfaces
{
    public interface IUserService
    {
        // Commands
        Task<UserViewDTO> CreateAsyncService(UserCreateDTO entity);
        Task<UserViewDTO> UpdateService(Guid id, UserUpdateNameOrMailOrPasswordDTO entity);
        Task DeleteService(Guid id);

        Task<bool> ChangeRoleService(Guid id, UserChangeRoleDTO role);

        // Queries
        Task<ICollection<UserViewDTO>> GetAllAsyncService();
        Task<UserViewDTO> GetByIdAsyncService(Guid id);
        Task<ICollection<UserViewDTO>> GetByNameAsyncService(string name);
        Task<UserViewDTO> GetByEmailAsyncService(string email);
        Task<ICollection<UserViewDTO>> GetByRoleAsyncService(string role);
    }
}
