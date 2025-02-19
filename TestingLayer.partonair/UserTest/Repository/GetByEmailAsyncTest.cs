using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;


namespace TestingLayer.partonair.UserTest.Repository
{
    public class GetByEmailAsyncTest : BaseUserInfrastructureTestFixture
    {
        [Fact]
        public async Task GetByEmailAsync_ExistingEmail_ReturnsUser()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser" , PasswordHashed = "passHash"};
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();           

            // Act
            var result = await _userRepo.GetByEmailAsync("test@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldThrow_Infrastructure_ResourceNotFoundException()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash" };
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _userRepo.GetByEmailAsync("bad@mail.kc"));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);           
        }
    }
}
