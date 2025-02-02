using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;

namespace DomainLayer.partonair.Contracts
{
    public interface IUser
    {
        #region Commands - Write
        /// <summary>
        /// Insert into Database a new User.
        /// </summary>
        /// <param name="user">Object which represent User</param>
        /// <returns>Returns asynchronous task.</returns>
        Task InsertAsync(User user);

        /// <summary>
        /// Update an existing User in Database.
        /// </summary>
        /// <param name="user">Object which represent User to update</param>
        /// <returns>Returns asynchronous task.</returns>
        Task UpdateAsync(User user);

        /// <summary>
        /// Delete an existing User from Database.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Returns asynchronous task.</returns>
        Task DeleteAsync(Guid id);
        #endregion


        #region Queries - Read
        /// <summary>
        /// Fetch one User by identifier.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Returns an asynchronous task with the existing User in database</returns>
        Task<User> GetByIdAsync(Guid id);

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
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        /// Fetch Users by an enumerator Roles.
        /// </summary>
        /// <param name="role">The role searched</param>
        /// <returns>Returns an asynchronous task list of User wich matched by Role</returns>
        Task<ICollection<User>> GetByRoleAsync(Roles role);

        /// <summary>
        /// Fetch all user existing in database
        /// </summary>
        /// <returns>Returns asynchronous task list of User present in database</returns>
        Task<ICollection<User>> GetAllAsync();
        #endregion
    }
}
