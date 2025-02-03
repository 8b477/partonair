using DomainLayer.partonair.Entities;

namespace ApplicationLayer.partonair.DTOs
{
    public record UserViewDTO
    (
        Guid Id,
        string UserName,
        string Email,
        bool IsPublic,
        DateTime UserCreatedAt,
        DateTime LastConnection,
        string Role,
        Guid? FK_Profile
    );
}
