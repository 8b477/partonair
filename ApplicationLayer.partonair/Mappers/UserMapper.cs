using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;

namespace ApplicationLayer.partonair.Mappers
{
    internal static class UserMapper
    {
        internal static User ToEntity(this UserCreateDTO u)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Email = u.Email,
                UserName = u.UserName,
                PasswordHashed = u.Password,
                LastConnection = DateTime.Now,
                UserCreatedAt = DateTime.Now,
                Role = Roles.Visitor,
                IsPublic = false,
                FK_Profile = null,
                Profile = null
            };
        }

        internal static UserViewDTO ToView(this User u)
        {
            return new UserViewDTO
                (
                    u.Id,
                    u.UserName,
                    u.Email,
                    u.IsPublic,
                    u.UserCreatedAt,
                    u.LastConnection,
                    u.Role.ToString(),
                    u.FK_Profile
                );
        }


        internal static User ToEntity(this UserUpdateNameOrMailOrPasswordDTO u, User existingUser)
        {
            return new User
            {
                Id = existingUser.Id,
                FK_Profile = existingUser.FK_Profile,
                Profile = existingUser.Profile,
                IsPublic = existingUser.IsPublic,
                LastConnection = existingUser.LastConnection,
                Role = existingUser.Role,
                UserCreatedAt = existingUser.UserCreatedAt,

                Email = u.Email ?? existingUser.Email,
                UserName = u.UserName ?? existingUser.UserName,
                PasswordHashed = u.NewPassword ?? existingUser.PasswordHashed
            };
        }

    }
}
