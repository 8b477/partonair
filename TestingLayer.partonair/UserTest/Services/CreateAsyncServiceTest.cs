using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.MediatR.Commands.Users;
using ApplicationLayer.partonair.Services;

using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Microsoft.Extensions.Logging;

using Moq;

using System.Reflection.Metadata;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Services
{
    public class CreateAsyncServiceTest : UserBaseClassTest
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly Mock<IBCryptService> _mockBCrypt;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _userService;

        public CreateAsyncServiceTest():base()
        {
            _mockUserRepo = new ();
            _mockBCrypt = new();
            _mockLogger = new();
            _mockUoW.Setup(uow => uow.Users).Returns(_mockUserRepo.Object);
            _userService = new UserService(_mockUoW.Object, _mockBCrypt.Object, _mockLogger.Object);

        }


        [Fact]
        public async Task CreateAsyncService_ShouldCreateUser_WhenEmailIsAvailable()
        {
            // Arrange
            var userDto = new UserCreateDTO(UserConstants.NAME, Email: UserConstants.EMAIL, UserConstants.PASSWORD);
            var userEntity = new User
            {
                Id = Guid.NewGuid(),
                UserName = userDto.UserName,
                Email = userDto.Email,
                PasswordHashed = UserConstants.PASSWORD_HASHED
            };
            var userView = new UserViewDTO(
                userEntity.Id,
                userEntity.UserName,
                userEntity.Email,
                false,
                DateTime.Now,
                DateTime.Now,
                UserConstants.ROLE_VISITOR,
                null
            );

            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(userDto.Email)).ReturnsAsync(true);
            _mockBCrypt.Setup(bc => bc.HashPass(userDto.Password, 13)).Returns(UserConstants.PASSWORD_HASHED);
            _mockUserRepo.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(userEntity);
            _mockUoW.Setup(uow => uow.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);

            // Act
            var result = await _userService.CreateAsyncService(userDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

            Assert.Equal(userDto.Email, result.Email);
            Assert.Equal(userDto.Email, result.Email);

            _mockUserRepo.Verify(repo => repo.IsEmailAvailableAsync(userDto.Email), Times.Once);
            _mockBCrypt.Verify(bc => bc.HashPass(userDto.Password, 13), Times.Once);
            _mockUserRepo.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task CreateAsyncService_ShouldThrowException_WhenEmailIsNotAvailable()
        {
            // Arrange
            var userDto = new UserCreateDTO(UserConstants.NAME, Email: UserConstants.EMAIL, UserConstants.PASSWORD);

            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(userDto.Email))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(
                () => _userService.CreateAsyncService(userDto)
            );

            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            // Verify
            _mockUserRepo.Verify(repo => repo.IsEmailAvailableAsync(userDto.Email), Times.Once);
            _mockUserRepo.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Never);
        }


        [Fact]
        public void CreateAsyncService_ShouldThrowExceptionWhenPasswordIsNotCorrect_SaltParseBCryptException()
        {
            // Arrange
            string badPassword = "";

            _mockBCrypt.Setup(s => s.HashPass(badPassword, 13))
                .Throws(new ApplicationLayerException(ApplicationLayerErrorType.SaltParseBCryptException));

            // Act & Assert
            var exception = Assert.Throws<ApplicationLayerException>(
                () => _mockBCrypt.Object.HashPass(badPassword, 13));
            

            Assert.Equal(ApplicationLayerErrorType.SaltParseBCryptException, exception.ErrorType);

            // Verify
            _mockBCrypt.Verify(v => v.HashPass(badPassword, 13), Times.Once);
        }
        

        [Fact]
        public async Task CreateAsyncService_ShouldThrowExceptionWhenPasswordIsNotCorrect_UnexpectedDatabaseException()
        {
            // Arrange

            _mockUoW.Setup(s => s.SaveChangesAsync(CancellationToken.None))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.UnexpectedDatabaseException));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(
                () => _mockUoW.Object.SaveChangesAsync(CancellationToken.None));
            

            Assert.Equal(InfrastructureLayerErrorType.UnexpectedDatabaseException, exception.ErrorType);

            // Verify
            _mockUoW.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task CreateAsyncService_WhenDatabaseOperationCanceled_ShouldThrowInfrastructureLayerException()
        {
            // Arrange
            var userCreateDto = new UserCreateDTO(UserConstants.NAME, UserConstants.EMAIL, UserConstants.PASSWORD);

            _mockUserService.Setup(s => s.CreateAsyncService(It.IsAny<UserCreateDTO>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException, "Database operation canceled"));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _mockUserService.Object.CreateAsyncService(userCreateDto));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);
        }

    }
}