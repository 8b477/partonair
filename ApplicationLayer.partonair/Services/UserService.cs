using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Exceptions.Enums;
using ApplicationLayer.partonair.Exceptions;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Mappers;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Contracts;


namespace ApplicationLayer.partonair.Services
{
    public class UserService(IUnitOfWork UOW, IBCryptService bCryptService) : IUserService
    {
        private readonly IUnitOfWork _UOW = UOW;
        private readonly IBCryptService _bCryptService = bCryptService;

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

        public async Task<ICollection<UserViewDTO>> GetAllAsync()
        {
            var result = await _UOW.Users.GetAllAsync();

            return result.Select(user => user.ToView())
                                                     .ToList();
        }

        public async Task<UserViewDTO> GetUserByIdAsync(Guid id)
        {
            var user = await _UOW.Users.GetByIdAsync(id);

            return user.ToView();
        }
    }    
}
