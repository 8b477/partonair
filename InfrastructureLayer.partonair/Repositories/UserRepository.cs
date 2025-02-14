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
            var result = await _dbSet.Where(u  => u.Email == email)
                                         .FirstOrDefaultAsync();

            return 
                result
                ??
                throw new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException, $"The mail : {email} - no match");
        }

        public async Task<ICollection<User>> GetByNameAsync(string name)
        {
            var result = await _dbSet.Where(u => u.UserName == name)
                                         .ToListAsync();

            return 
                result
                ??
                throw new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException, $"The name : {name} - no match");
        }

        public async Task<ICollection<User>> GetByRoleAsync(string role)
        {
            var result = await _dbSet.Where(u => u.Role.ToString() == role)
                                         .ToListAsync();

            return 
                result
                ??
                throw new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException, $"The role : {role} - no match");
        }

        #endregion



        #region CHECK

        public async Task<bool> IsEmailAvailableAsync(string email)
        {
            var result = await _dbSet.Where(u => u.Email == email)
                                         .AnyAsync();

            return !result ;
        }


        public async Task<bool> IsUserWithoutProfil(Guid id)
        {
            var existingUser = await _dbSet.Include(u => u.Profile).FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException, $"Identifier : {id} - No match");

            var dbg = existingUser.Profile;

            return 
                existingUser.Profile is null
                ? true
                : false; 
        }
        #endregion



        #region Update

        public async Task<bool> ChangeRoleAsync(Guid id, string role)
        {
            var existingUser = await _dbSet.FindAsync(id)
                ?? throw new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException, $"Identifier : {id} - No match");

            Enum.TryParse<Roles>(role,true,out var roleToAdd);

            existingUser.Role = roleToAdd;

            return true;
        }

        #endregion

    }
}
