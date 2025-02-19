

using DomainLayer.partonair.Entities;

using Microsoft.EntityFrameworkCore;

namespace TestingLayer.partonair.UserTest.Repository
{
    public class IsEmailAvailableAsyncTest : BaseUserInfrastructureTestFixture
    {
        [Fact]
        public async Task IsEmailAvaibleAsync_ShouldReturn_True()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash" };
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();

            var result = await _userRepo.IsEmailAvailableAsync("notExisting@mail.kc");

            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task IsEmailAvaibleAsync_ShouldReturn_False()
        {
            // Arrange
            var testUser = new User { Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash" };
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();


            var result = await _userRepo.IsEmailAvailableAsync("test@example.com");

            Assert.False(result);
            Assert.IsType<bool>(result);
        }

    }
}
