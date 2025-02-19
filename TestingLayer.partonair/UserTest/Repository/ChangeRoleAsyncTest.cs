using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;


namespace TestingLayer.partonair.UserTest.Repository
{
    public class ChangeRoleAsyncTest : BaseUserInfrastructureTestFixture
    {
        [Fact]
        public async Task ChangeRoleAsync_ShouldReturn_True()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var testUser = new User { Id = id, Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();

            string newRole = "Employee";

            // Act
            var result = await _userRepo.ChangeRoleAsync(id, newRole);
            var updated = await _ctx.Users.FindAsync(id);

            // Assert
            Assert.True(result);
            Assert.Equal(newRole, updated?.Role.ToString());
        }

        [Fact]
        public async Task ChangeRoleAsync_ShouldReturn_False()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var testUser = new User { Id = id, Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();

            string newRole = "bad role";

            // Act
            var result = await _userRepo.ChangeRoleAsync(id, newRole);
            var updated = await _ctx.Users.FindAsync(id);

            // Assert
            Assert.False(result);
            Assert.NotEqual(newRole, updated?.Role.ToString());
        }

        [Fact]
        public async Task ChangeRoleAsync_ShouldThrow_Infrastructure_EntityIsNullException()
        {
            // Arrange
            Guid badId = Guid.Empty;
            var testUser = new User { Id = badId, Email = "test@example.com", UserName = "TestUser", PasswordHashed = "passHash", Role = Roles.Visitor };
            _ctx.Add(testUser);
            await _ctx.SaveChangesAsync();

            string newRole = "Employee";

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _userRepo.ChangeRoleAsync(badId, newRole));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);
        }

    }
}