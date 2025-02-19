using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.partonair.Entities;

namespace TestingLayer.partonair.UserTest.Repository
{
    public class IsUserWithoutProfilTest : BaseUserInfrastructureTestFixture
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly User _testUser;

        public IsUserWithoutProfilTest()
        {
            _testUser = new User
                        {
                            Id = _id,
                            Email = "test@example.com",
                            UserName = "TestUser",
                            PasswordHashed = "passHash",
                            Profile = null,
                            FK_Profile = null
                        };      
        }

        [Fact]
        public async Task IsUserWithoutProfil_ShouldReturn_True()
        {
            // Arrange
            _ctx.Add(_testUser);
            await _ctx.SaveChangesAsync();

            var result = await _userRepo.IsUserWithoutProfil(_testUser.Id);

            // Assert
            Assert.True(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task IsUserWithoutProfil_ShouldReturn_False()
        {
            // Arrange
            Guid idProfile = Guid.NewGuid();

            var profileUserTest = new Profile { Id = idProfile, FK_User = _id, User = _testUser , ProfileDescription = "test du user avec profile"};
            _testUser.FK_Profile = profileUserTest.Id;
            _testUser.Profile = profileUserTest;

            _ctx.Add(_testUser);
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _userRepo.IsUserWithoutProfil(_testUser.Id);

            // Assert
            Assert.False(result);
            Assert.IsType<bool>(result);
        }

        [Fact]
        public async Task IsUserWithoutProfil_ShouldThrow_Infrastructure_EntityIsNullException()
        {
            // Arrange
            Guid badId = Guid.Empty;
            _ctx.Add(_testUser);
            await _ctx.SaveChangesAsync();

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                =>_userRepo.IsUserWithoutProfil(badId));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);
        }

    }
}
