using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Services
{
    public class CreateAsyncServiceTest : BaseUserApplicationServiceTestFixture
    {
        private readonly UserCreateDTO _dto = new(UserConstants.NAME, UserConstants.EMAIL, UserConstants.PASSWORD);
        private readonly User _entity = new() { Id = Guid.NewGuid(), UserName = UserConstants.NAME, Email = UserConstants.EMAIL, IsPublic = false, Profile = null, FK_Profile = null, LastConnection = DateTime.Now, UserCreatedAt = DateTime.Now, PasswordHashed = UserConstants.PASSWORD_HASHED,Role = DomainLayer.partonair.Enums.Roles.Visitor};


        private void SetupMocksForSuccessfulCreateUser()
        {
            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>())).ReturnsAsync(true);
            _mockBCrypt.Setup(bc => bc.HashPass(It.IsAny<string>(), 13)).Returns(UserConstants.PASSWORD_HASHED);
            _mockUserRepo.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(_entity);
            _mockUoW.Setup(uow => uow.SaveChangesAsync(CancellationToken.None)).ReturnsAsync(1);
        }

        private void VerifyAllMockIsCalled()
        {
            _mockUserRepo.Verify(repo => repo.IsEmailAvailableAsync(It.IsAny<string>()), Times.Once);
            _mockBCrypt.Verify(bc => bc.HashPass(It.IsAny<string>(), 13), Times.Once);
            _mockUserRepo.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        }


        [Fact]
        public async Task CreateAsyncService_ShouldCreateUser_WhenEmailIsAvailable()
        {
            // Arrange
            SetupMocksForSuccessfulCreateUser();

            // Act
            var result = await _userService.CreateAsyncService(_dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

            VerifyAllMockIsCalled();
        }


        [Fact]
        public async Task CreateAsyncService_ShouldThrowException_WhenEmailIsNotAvailable()
        {
            // Arrange
            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(
                () => _userService.CreateAsyncService(_dto)
            );

            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            // Verify
            _mockUserRepo.Verify(repo => repo.IsEmailAvailableAsync(It.IsAny<string>()), Times.Once);
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
            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(It.IsAny<string>()))
                         .ReturnsAsync(true);

            _mockUserRepo.Setup(s => s.CreateAsync(It.IsAny<User>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _userService.CreateAsyncService(_dto));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.IsEmailAvailableAsync(It.IsAny<string>()),Times.Once);
        }

    }
}