using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.MediatR.Commands.Users;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Commands
{
    public class AddUserCommandTest : UserBaseClassTest
    {
        private readonly AddUserCommandHandler _handler;
        private readonly Mock<IBCryptService> _mockBCrypt;
        public AddUserCommandTest() : base()
        {
            _handler = new AddUserCommandHandler(_mockUserService.Object);
            _mockBCrypt = new Mock<IBCryptService>();
        }

        [Fact]
        public async Task AddUserCommandHandler_ShouldReturnsCreatedUser()
        {

            var userCreateDto = new UserCreateDTO
                (
                UserConstants.Name,
                UserConstants.Email,
                UserConstants.Password
                );

            var expectedUser = new UserViewDTO
                (
                    new Guid(),
                   userCreateDto.UserName,
                   userCreateDto.Email,
                   false,
                   DateTime.Now,
                   DateTime.Now,
                   "Visitor",
                   null
                );

            _mockUserService.Setup(s => s.CreateAsyncService(It.Is<UserCreateDTO>
                                      (p => p.UserName == UserConstants.Name &&
                                                  p.Email == UserConstants.Email
                                      ))
                                  ).ReturnsAsync(expectedUser);

            var result = await _handler.Handle(new AddUserCommand(userCreateDto),CancellationToken.None);

            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);
            Assert.Equal(expectedUser.UserName, result.UserName);
            Assert.Equal(expectedUser.Email, result.Email);

            _mockUserService.Verify(v => v.CreateAsyncService(It.IsAny<UserCreateDTO>()), Times.Once());
        }
    }
}


/*
         public async Task CreateAsyncService_ShouldReturnsCreatedUser()
        {

            // ---------------> Arrange <---------------

            var userCreateDto = new UserCreateDTO
                (
                UserConstants.Name,
                UserConstants.Email,
                UserConstants.Password
                );
            _mockUoW.Setup(s => s.Users.IsEmailAvailableAsync(userCreateDto.Email))
                    .ReturnsAsync(true);

            _mockUoW.Verify(v => v.Users.IsEmailAvailableAsync(userCreateDto.Email),Times.Once);

            var user = new User
            {
                Id = Guid.NewGuid(), // In real case Id is generate by Database
                UserName = userCreateDto.UserName,
                Email = userCreateDto.Email,
                PasswordHashed = userCreateDto.Password,
                Role = Roles.Visitor,
                UserCreatedAt = DateTime.UtcNow,
                LastConnection = DateTime.UtcNow,
                IsPublic = false,
                FK_Profile = null,
                Profile = null
            };


            _mockBCrypt.Setup(s => s.HashPass(userCreateDto.Password,13))
                       .Returns(UserConstants.PasswordHashed);

            user.PasswordHashed = UserConstants.PasswordHashed;


            var expectedUser = new UserViewDTO
                (
                    user.Id,
                   user.UserName,
                   user.Email,
                   user.IsPublic,
                   user.UserCreatedAt,
                   user.LastConnection,
                   user.Role.ToString(),
                   user.FK_Profile
                );

            var command = new AddUserCommand(userCreateDto);

            _mockUserService.Setup(s => s.CreateAsyncService(It.IsAny<UserCreateDTO>()))
                            .ReturnsAsync(expectedUser);

            _mockUserService.Verify<UserViewDTO>(v => v.CreateAsyncService(userCreateDto).Result, Times.Once);
        }
 */