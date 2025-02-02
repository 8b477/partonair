using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;

using Infrastructure.partonair.Enums;
using InfrastructureLayer.partonair.Exceptions;
using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.partonair.Repositories
{
    public class UserRepository : IUser
    {

        // <--------------------------------> TODO <-------------------------------->
        // 
        // <--------------------------------> **** <-------------------------------->



        #region DI

        private readonly AppDbContext _ctx;
        public UserRepository(AppDbContext ctx) => _ctx = ctx;

        #endregion



        #region <-------------> CREATE <------------->

        public Task InsertAsync(User user)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region <-------------> GET <------------->

        public Task<ICollection<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
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

            if (user is null)
                throw new InfrastructureException(InfrastructureErrorType.ResourceNotFound, $"Identifier User: {id} - no match");

            return user;
        }

        public Task<ICollection<User>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetByRoleAsync(Roles role)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region <-------------> UPDATE <------------->

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region <-------------> DELETE <------------->

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
