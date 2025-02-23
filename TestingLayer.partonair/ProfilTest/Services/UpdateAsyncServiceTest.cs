

using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;

namespace TestingLayer.partonair.ProfilTest.Services
{
    public class UpdateAsyncServiceTest : BaseProfileApplicationServiceTestFixture
    {
        private readonly User _existingUser;
        private readonly Profile _existingProfile;
        public UpdateAsyncServiceTest()
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

            _existingProfile = new()
            {
                Id = Guid.NewGuid(),
                ProfileDescription = "super description for the super test",
                FK_User = _existingUser.Id,
                User = _existingUser
            };

        }


        [Fact]
        public async Task UpdateAsyncService_ShouldReturn_Success_ProfileViewDTO()
        {
            // Arrange
            ProfileUpdateDTO dto = new("new description");

            Profile updatedProfile = new()
            {
                Id = _existingProfile.Id,
                ProfileDescription = dto.ProfilDescritpion,
                FK_User = _existingUser.Id,
                User = _existingUser
            };

            _mockUnitOfWork.Setup(s => s.Profiles.GetByGuidAsync(_existingProfile.Id)).ReturnsAsync(_existingProfile);
            _mockProfileRepo.Setup(s => s.Update(It.IsAny<Profile>())).ReturnsAsync(updatedProfile);
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));

            // Act
            var result = await _profileService.UpdateAsyncService(_existingProfile.Id, dto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProfileViewDTO>(result);
            Assert.Equal(dto.ProfilDescritpion,result.ProfilDescritpion);

            _mockUnitOfWork.Verify(v => v.Profiles.GetByGuidAsync(_existingProfile.Id),Times.Once);
            _mockProfileRepo.Verify(v => v.Update(It.IsAny<Profile>()), Times.Once);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None),Times.Once);
        }

        [Fact]
        public async Task UpdateAsyncService_ShouldThrow_InfrastructureLayerErrorType_ResourceNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.Profiles.GetByGuidAsync(Guid.Empty))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            _mockProfileRepo.Setup(s => s.Update(It.IsAny<Profile>())).ReturnsAsync(It.IsAny<Profile>());
            _mockUnitOfWork.Setup(s => s.SaveChangesAsync(CancellationToken.None));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                =>_profileService.UpdateAsyncService(Guid.Empty, It.IsAny<ProfileUpdateDTO>()));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);
         
            _mockUnitOfWork.Verify(v => v.Profiles.GetByGuidAsync(Guid.Empty), Times.Once);
            _mockProfileRepo.Verify(v => v.Update(It.IsAny<Profile>()), Times.Never);
            _mockUnitOfWork.Verify(v => v.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

    }
}
