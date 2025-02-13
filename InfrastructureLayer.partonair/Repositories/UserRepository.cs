using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;
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
                throw new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException, $"The mail : {email} - no match");
        }

        public async Task<ICollection<User>> GetByNameAsync(string name)
        {
            var result = await _ctx.Users.Where(u => u.UserName == name)
                                         .ToListAsync();

            return 
                result
                ??
                throw new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException, $"The name : {name} - no match");
        }

        public async Task<ICollection<User>> GetByRoleAsync(string role)
        {
            var result = await _ctx.Users.Where(u => u.Role.ToString() == role)
                                         .ToListAsync();

            return 
                result
                ??
                throw new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException, $"The role : {role} - no match");
        }

        #endregion


        public async Task<bool> IsEmailAvailable(string email)
        {
            var result = await _ctx.Users.Where(u => u.Email == email)
                                         .AnyAsync();

            return !result ;
        }
    }
}
