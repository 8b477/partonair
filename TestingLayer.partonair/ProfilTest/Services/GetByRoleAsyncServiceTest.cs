using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ProfilTest.Services
{
    public class GetByRoleAsyncServiceTest : BaseProfileApplicationServiceTestFixture
    {
        private readonly List<User> _users;
        public GetByRoleAsyncServiceTest()
        {
            _users =
            [
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserName = "User1",
                    Email = "user1@example.com",
                    Role = Roles.Visitor,
                    Profile = new Profile
                    {
                        Id = Guid.NewGuid(),
                        ProfileDescription = "Profile 1"
                    }
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserName = "User2",
                    Email = "user2@example.com",
                    Role = Roles.Visitor,
                    Profile = null
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserName = "User3",
                    Email = "user3@example.com",
                    Role = Roles.Employee,
                    Profile = new Profile
                    {
                        Id = Guid.NewGuid(),
                        ProfileDescription = "Profile 3"
                    }
                }
            ];
        }

        [Fact]
        public async Task GetByRoleAsyncService_ShouldReturn_Success_ProfileAndUserViewDTOList()
        {
            // Arrange
            string testRole = "Visitor";

            _mockUserRepo.Setup(s => s.GetByRoleIncludeProfilAsync(testRole)).ReturnsAsync(_users);

            // Act
            var result = await _profileService.GetByRoleAsyncService(testRole);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ProfileAndUserViewDTO>>(result);
            Assert.Equal(2, result.Where(p => p.User.Role.ToString() == testRole).Count());

            _mockUserRepo.Verify(v => v.GetByRoleIncludeProfilAsync(testRole),Times.Once);
        }

        [Fact]
        public async Task GetByRoleAsyncService_ShoulThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            string testRole = "Bad role";

            _mockUserRepo.Setup(s => s.GetByRoleIncludeProfilAsync(testRole)).ReturnsAsync(_users);

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                =>_profileService.GetByRoleAsyncService(testRole));

            // Assert
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.GetByRoleIncludeProfilAsync(testRole), Times.Never);
        }


        [Fact]
        public async Task GetByRoleAsyncService_ShoulThrow_InfrastructureLayerErrorType_ResourceNotFoundException()
        {
            // Arrange
            string testRole = "company";

            _mockUserRepo.Setup(s => s.GetByRoleIncludeProfilAsync(testRole))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _profileService.GetByRoleAsyncService(testRole));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.GetByRoleIncludeProfilAsync(testRole), Times.Once);
        }

    }
}