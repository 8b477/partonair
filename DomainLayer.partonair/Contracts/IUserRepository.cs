using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;

namespace DomainLayer.partonair.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        /// <summary>
        /// Fetch a list of User by Name. 
        /// </summary>
        /// <param name="name">Represent the filter to apply</param>
        /// <returns>Returns an asynchronous task collection of User filtered by name</returns>
        Task<ICollection<User>> GetByNameAsync(string name);

        /// <summary>
        /// Fetch one User by Email.
        /// </summary>
        /// <param name="email">The email searched</param>
        /// <returns>Returns an asynchronous task with the matched User</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Fetch Users by an enumerator Roles.
        /// </summary>
        /// <param name="role">The role searched</param>
        /// <returns>Returns an asynchronous task list of User wich matched by Role</returns>
        Task<ICollection<User>> GetByRoleAsync(string role);

        /// <summary>
        /// Checks if the specified email already exists in the database.
        /// </summary>
        /// <param name="email">The email address to check.</param>
        /// <returns>
        /// <c>true</c> if the email does not exist in the database; otherwise, <c>false</c>.
        /// </returns>
        Task<bool> IsEmailAvailable(string email);
    }
}
