using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;


namespace TestingLayer.partonair.UserTest.Repository
{
    public class GetByNameAsyncTest : BaseUserInfrastructureTestFixture
    {
        [Fact]
        public async Task GetByNameAsync_ExistingName_ReturnsUsers()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash" };
            var testUser2 = new User { Email = "test2@example.com", UserName = "TestUser", PasswordHashed = "passHash" };

            _ctx.AddRange(testUser,testUser2);
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _userRepo.GetByNameAsync("TestUser");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<User>>(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldThrow_Infrastructure_ResourceNotFoundException()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash" };
            var testUser2 = new User { Email = "test2@example.com", UserName = "TestUser", PasswordHashed = "passHash" };

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