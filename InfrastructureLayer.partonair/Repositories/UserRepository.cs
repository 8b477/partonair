using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;

using InfrastructureLayer.partonair.Enums;
using InfrastructureLayer.partonair.Exceptions;
using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;

using System.Data;


namespace InfrastructureLayer.partonair.Repositories
{
    public class UserRepository(AppDbContext ctx) : GenericRepository<User>(ctx), IUserRepository
    {

        #region <-------------> GET <------------->

        public async Task<User> GetByEmailAsync(string email)
        {
            var result = await _ctx.Users.Where(u  => u.Email == email)
                                         .FirstOrDefaultAsync();

            return 
                result
                ??
                throw new InfrastructureException(InfrastructureErrorType.ResourceNotFound, $"The mail : {email} - no match");
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _ctx.Users
                            .Select(u => new User
                            {
                                Id = u.Id,
                                UserName = u.UserName,
                                Email = u.Email,
                                IsPublic = u.IsPublic,
                                UserCreatedAt = u.UserCreatedAt,
                                LastConnection = u.LastConnection,
                                Role = u.Role,
                                FK_Profile = u.FK_Profile
                            })
                            .FirstOrDefaultAsync(u => u.Id == id);

            return 
                user 
                ??
                throw new InfrastructureException(InfrastructureErrorType.ResourceNotFound, $"Identifier User: {id} - no match");
        }

        public async Task<ICollection<User>> GetByNameAsync(string name)
        {
            var result = await _ctx.Users.Where(u => u.UserName == name)
                                         .ToListAsync();

            return 
                result
                ??
                throw new InfrastructureException(InfrastructureErrorType.ResourceNotFound, $"The name : {name} - no match");
        }

        public async Task<ICollection<User>> GetByRoleAsync(string role)
        {
            var result = await _ctx.Users.Where(u => u.Role.ToString() == role)
                                         .ToListAsync();

            return 
                result
                ??
                throw new InfrastructureException(InfrastructureErrorType.ResourceNotFound, $"The role : {role} - no match");
        }

        #endregion

    }
}
