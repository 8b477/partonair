using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.ProfilTest.Services
{
    public class CreateAsyncServiceTest : BaseProfileApplicationServiceTestFixture
    {
        private readonly User _existingUser;
        private readonly User _userUpdated;
        private readonly Profile _createdProfile;
        private readonly ProfileCreateDTO _dto;

        public CreateAsyncServiceTest()
        {
            _existingUser = new()
            {
                Id = Guid.NewGuid(),
                UserName = UserConstants.NAME,
                Email = UserConstants.EMAIL,
                FK_Profile = null,
                Profile = null,
                IsPublic = false,
                LastConnection = DateTime.Now,
                UserCreatedAt = DateTime.Now,
                PasswordHashed = UserConstants.PASSWORD_HASHED,
                Role = Roles.Visitor
            };

            _dto = new("Super description test");

            _createdProfile = new Profile
            {
                Id = Guid.NewGuid(),
                ProfileDescription = _dto.ProfileDescription,
                FK_User = _existingUser.Id,
                User = _existingUser
            };

            _userUpdated = new User()
            {
                Id = _existingUser.Id,
                UserName = _existingUser.UserName,
                Email = _existingUser.Email,
                IsPublic = _existingUser.IsPublic,
                LastConnection = _existingUser.LastConnection,
                UserCreatedAt = _existingUser.UserCreatedAt,
                PasswordHashed = _existingUser.PasswordHashed,
                Role = _existingUser.Role,

                FK_Profile = _createdProfile.Id,
                Profile = _createdProfile
            };
        }

        [Fact]
        public async Task CreateAsyncService_ShouldReturn_Success_ProfileViewDTO()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _mockUserRepo.Setup(s => s.GetByGuidAsync(_existingUser.Id)).ReturnsAsync(_existingUser);
            _mockProfileRepo.Setup(s => s.CreateAsync(It.IsAny<Profile>())).ReturnsAsync(_createdProfile);
            _mockUserRepo.Setup(s => s.Update(_existingUser)).ReturnsAsync(_userUpdated);
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));
            _mockUnitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var result = await _profileService.CreateAsyncService(_existingUser.Id, _dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProfileViewDTO>(result);

            _mockUnitOfWork.Verify(v => v.BeginTransactionAsync(),Times.Once);
            _mockUserRepo.Verify(v => v.GetByGuidAsync(_existingUser.Id), Times.Once);
            _mockProfileRepo.Verify(v => v.CreateAsync(It.IsAny<Profile>()), Times.Once);
            _mockUserRepo.Verify(v => v.Update(_existingUser), Times.Once);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None),Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitTransactionAsync(),Times.Once);
        }

        [Fact]
        public async Task CreateAsyncService_ShouldThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _mockUserRepo.Setup(s => s.GetByGuidAsync(_existingUser.Id)).ReturnsAsync(_existingUser);
            _mockProfileRepo.Setup(s => s.CreateAsync(It.IsAny<Profile>())).ReturnsAsync(_createdProfile);
            _mockUserRepo.Setup(s => s.Update(_existingUser)).ReturnsAsync(_userUpdated);
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));
            _mockUnitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            _existingUser.Profile = _createdProfile;
            _existingUser.FK_Profile = _createdProfile.Id;

            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _profileService.CreateAsyncService(_existingUser.Id, _dto));

            // Assert
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockUnitOfWork.Verify(v => v.BeginTransactionAsync(), Times.Once);
            _mockUserRepo.Verify(v => v.GetByGuidAsync(_existingUser.Id), Times.Once);
            _mockProfileRepo.Verify(v => v.CreateAsync(It.IsAny<Profile>()), Times.Never);
            _mockUserRepo.Verify(v => v.Update(_existingUser), Times.Never);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitTransactionAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsyncService_ShouldThrow_InfrastructureLayerErrorType_UpdateDatabaseException()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _mockUserRepo.Setup(s => s.GetByGuidAsync(_existingUser.Id)).ReturnsAsync(_existingUser);
            _mockProfileRepo.Setup(s => s.CreateAsync(It.IsAny<Profile>())).ReturnsAsync(_createdProfile);
            _mockUserRepo.Setup(s => s.Update(_existingUser)).ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.UpdateDatabaseException));
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));
            _mockUnitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _profileService.CreateAsyncService(_existingUser.Id, _dto));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.UpdateDatabaseException, exception.ErrorType);

            _mockUnitOfWork.Verify(v => v.BeginTransactionAsync(), Times.Once);
            _mockUserRepo.Verify(v => v.GetByGuidAsync(_existingUser.Id), Times.Once);
            _mockProfileRepo.Verify(v => v.CreateAsync(It.IsAny<Profile>()), Times.Once);
            _mockUserRepo.Verify(v => v.Update(_existingUser), Times.Once);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitTransactionAsync(), Times.Never);
        }

    }
}