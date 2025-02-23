using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.ProfilTest.Services
{
    public class GetByGuidAsyncServiceTest : BaseProfileApplicationServiceTestFixture
    {
        private readonly User _existingUser;
        private readonly Profile _existingProfile;
        public GetByGuidAsyncServiceTest()
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
        public async Task GetByGuidAsyncService_ShouldReturn_Succes_ProfileViewDTO()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.Profiles.GetByGuidAsync(_existingProfile.Id)).ReturnsAsync(_existingProfile);

            // Act
            var result = await _profileService.GetByGuidAsyncService(_existingProfile.Id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProfileViewDTO>(result);

            _mockUnitOfWork.Verify(v=> v.Profiles.GetByGuidAsync(_existingProfile.Id), Times.Once);
        }


        [Fact]
        public async Task GetByGuidAsyncService_ShouldThrow_InfrastructureLayerException_ResourceNotFoundException()
        {
            // Arrange
            _mockUnitOfWork.Setup(s => s.Profiles.GetByGuidAsync(_existingProfile.Id))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                =>_profileService.GetByGuidAsyncService(_existingProfile.Id));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUnitOfWork.Verify(v => v.Profiles.GetByGuidAsync(_existingProfile.Id), Times.Once);
        }

    }
}