using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Services
{
    public class CreateAsyncServiceTest : ExtendBaseUserApplicationServiceTestFixture
    {
        private readonly Guid _id;
        private readonly User _userEntity;
        private readonly UserCreateDTO _userCreateDTO;
        public CreateAsyncServiceTest()
        {
            _id = Guid.NewGuid();
            _userEntity = CreateUserEntity(_id);
            _userCreateDTO = CreateUserCreateDTO();
        }

        private static User CreateUserEntity(Guid id) => new User
        {
            Id = id,
            UserName = UserConstants.NAME,
            Email = UserConstants.EMAIL,
            PasswordHashed = UserConstants.PASSWORD_HASHED
        };

        private static UserCreateDTO CreateUserCreateDTO() => new UserCreateDTO
        (
            UserConstants.NAME,
            UserConstants.EMAIL,
            UserConstants.PASSWORD
        );

        private void SetupMocksForSuccessfulCreateUser()
        {
            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(_userCreateDTO.Email)).ReturnsAsync(true);
            _mockBCrypt.Setup(bc => bc.HashPass(_userCreateDTO.Password, 13)).Returns(UserConstants.PASSWORD_HASHED);
            _mockUserRepo.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(_userEntity);
            _mockUoW.Setup(uow => uow.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);
        }

        private void VerifyAllMockIsCalled()
        {
            _mockUserRepo.Verify(repo => repo.IsEmailAvailableAsync(_userCreateDTO.Email), Times.Once);
            _mockBCrypt.Verify(bc => bc.HashPass(_userCreateDTO.Password, 13), Times.Once);
            _mockUserRepo.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task CreateAsyncService_ShouldCreateUser_WhenEmailIsAvailable()
        {
            // Arrange
            SetupMocksForSuccessfulCreateUser();
            // Act
            var result = await _userService.CreateAsyncService(_userCreateDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

            Assert.Equal(_userCreateDTO.Email, result.Email);
            Assert.Equal(_userCreateDTO.Email, result.Email);

            VerifyAllMockIsCalled();
        }


        [Fact]
        public async Task CreateAsyncService_ShouldThrowException_WhenEmailIsNotAvailable()
        {
            // Arrange
            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(_userCreateDTO.Email))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(
                () => _userService.CreateAsyncService(_userCreateDTO)
            );

            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            // Verify
            _mockUserRepo.Verify(repo => repo.IsEmailAvailableAsync(_userCreateDTO.Email), Times.Once);
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
            _mockUserService.Setup(s => s.CreateAsyncService(It.IsAny<UserCreateDTO>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException, "Database operation canceled"));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _mockUserService.Object.CreateAsyncService(_userCreateDTO));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);
        }

    }
}