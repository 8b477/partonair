using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.ProfilTest.Services
{
    public class DeleteAsyncServiceTest : BaseProfileApplicationServiceTestFixture
    {
        private readonly User _existingUser;
        private readonly Profile _existingProfile;

        public DeleteAsyncServiceTest()
        {
            _existingUser = new()
            {
                Id = Guid.NewGuid(),
                UserName = UserConstants.NAME,
                Email = UserConstants.EMAIL,
                IsPublic = false,
                Profile = null,
                FK_Profile = null,
                LastConnection = DateTime.Now,
                UserCreatedAt = DateTime.Now,
                PasswordHashed = UserConstants.PASSWORD_HASHED,
                Role = Roles.Visitor
            };

            _existingProfile = new()
            {
                Id = Guid.NewGuid(),
                FK_User = _existingUser.Id,
                User = _existingUser,
                ProfileDescription = "Super description !"
            };

        }

        [Fact]
        public async Task DeleteAsyncService_ShouldReturn_Success_void()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.BeginTransactionAsync());
            _mockUnitOfWork.Setup(s => s.Profiles.Delete(_existingProfile.Id)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.Users.GetByForeignKeyProfilAsync(_existingProfile.Id)).ReturnsAsync(_existingUser);

            _existingUser.FK_Profile = null;
            _existingUser.Profile = null;

            _mockUnitOfWork.Setup(s => s.Users.Update(_existingUser)).ReturnsAsync(It.IsAny<User>());
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));
            _mockUnitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            await _profileService.DeleteAsyncService(_existingProfile.Id);

            // Assert
            _mockUnitOfWork.Verify(v => v.BeginTransactionAsync(), Times.Once);
            _mockUnitOfWork.Verify(v => v.Profiles.Delete(_existingProfile.Id), Times.Once);
            _mockUnitOfWork.Verify(v => v.Users.GetByForeignKeyProfilAsync(_existingProfile.Id), Times.Once);
            _mockUnitOfWork.Verify(v => v.Users.Update(_existingUser), Times.Once);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Once);
            _mockUnitOfWork.Verify(v => v.CommitTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsyncService_ShouldThrow_InfrastructureLayerErrorType_ResourceNotFoundException()
        {
            // Arrange
            Guid BadId = Guid.NewGuid();

            _mockUnitOfWork.Setup(s => s.BeginTransactionAsync());
            _mockUnitOfWork.Setup(s => s.Profiles.Delete(BadId)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(s => s.Users.GetByForeignKeyProfilAsync(BadId))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            _mockUnitOfWork.Setup(s => s.Users.Update(_existingUser)).ReturnsAsync(It.IsAny<User>());
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));
            _mockUnitOfWork.Setup(s => s.CommitTransactionAsync());

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
               =>_profileService.DeleteAsyncService(BadId));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUnitOfWork.Verify(v => v.BeginTransactionAsync(), Times.Once);
            _mockUnitOfWork.Verify(v => v.Profiles.Delete(BadId), Times.Once);
            _mockUnitOfWork.Verify(v => v.Users.GetByForeignKeyProfilAsync(BadId), Times.Once);
            _mockUnitOfWork.Verify(v => v.Users.Update(_existingUser), Times.Never);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Never);
            _mockUnitOfWork.Verify(v => v.CommitTransactionAsync(), Times.Never);
        }

    }
}
//InfrastructureLayerErrorType.ResourceNotFoundException

/*
 
  await _UOW.BeginTransactionAsync();

                await _UOW.Profiles.Delete(id);

                var existingUser = await _UOW.Users.GetByForeignKeyProfilAsync(id);

                existingUser.FK_Profile = null;
                existingUser.Profile = null;

                await _UOW.Users.Update(existingUser);

                await _UOW.SaveChangesAsync();
                await _UOW.CommitTransactionAsync();

 */