using ApplicationLayer.partonair.DTOs;

namespace ApplicationLayer.partonair.Interfaces
{
    public interface IUserService
    {
       Task<UserViewDTO> GetUserByIdAsync(Guid id);
    }
}
