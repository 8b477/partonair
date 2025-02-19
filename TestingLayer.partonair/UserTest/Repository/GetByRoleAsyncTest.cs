

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Enums;


namespace TestingLayer.partonair.UserTest.Repository
{
    public class GetByRoleAsyncTest : BaseUserInfrastructureTestFixture
    {
        [Fact]
        public async Task GetByRoleAsync_ExistingName_ReturnsUsers()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };
            var testUser2 = new User { Email = "test2@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };

            _ctx.AddRange(testUser, testUser2);
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _userRepo.GetByRoleAsync("Visitor");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByRoleAsync_ShouldThrow_Infrastructure_ResourceNotFoundException()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };
            var testUser2 = new User { Email = "test2@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };

            _ctx.AddRange(testUser, testUser2);
            await _ctx.SaveChangesAsync();

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _userRepo.GetByNameAsync("notFound"));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);
        }
    }
}