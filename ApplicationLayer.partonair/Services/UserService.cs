using ApplicationLayer.partonair.DTOs;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Contracts;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Mappers;


namespace ApplicationLayer.partonair.Services
{
    public class UserService(IUnitOfWork UOW, IBCryptService bCryptService) : IUserService
    {
        // <--------------------------------> TODO <-------------------------------->
        // Order By name A->Z : 'GetById()'
        // <--------------------------------> **** <-------------------------------->

        private readonly IUnitOfWork _UOW = UOW;
        private readonly IBCryptService _bCryptService = bCryptService;


        #region Commands
        public async Task<UserViewDTO> CreateAsync(UserCreateDTO user)
        {
            bool isValidMail = await _UOW.Users.IsEmailAvailable(user.Email);

            if (!isValidMail)
                throw new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationError);


            User newUser = user.ToEntity();

            newUser.PasswordHashed = _bCryptService.HashPass(newUser.PasswordHashed);


            var userAdded = await _UOW.Users.CreateAsync(newUser);

            await _UOW.SaveChangesAsync();


            return userAdded.ToView();
        }

        public void Update(UserUpdateDTO entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion



        #region Queries
        public async Task<ICollection<UserViewDTO>> GetAllAsync()
        {
            var result = await _UOW.Users.GetAllAsync();

            return result.Select(user => user.ToView())
                                                     .ToList();
        }

        public Task<User> GetByEmailAsyncService(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<UserViewDTO> GetByIdAsyncService(Guid id)
        {
            var userEntity = await _UOW.Users.GetByIdAsync(id);

            return userEntity.ToView();
        }

        public Task<ICollection<User>> GetByNameAsyncService(string name)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<User>> GetByRoleAsyncService(string role)
        {
            throw new NotImplementedException();
        }

        public async Task<UserViewDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _UOW.Users.GetByIdAsync(id);

            return user.ToView();
        }

        public Task<bool> IsEmailAvailableService(string email)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
