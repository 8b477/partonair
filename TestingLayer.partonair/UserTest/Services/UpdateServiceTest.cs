using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;

using Moq;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Services
{
    public class UpdateServiceTest : ExtendUserServiceTest
    {
        public UpdateServiceTest():base() 
        {

        }

        [Fact]
        public async Task UpdateService_ShouldUpdateUserCorrectly_AndReturnUserViewDTO()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var existingUser = new User { Id = userId, Email = UserConstants.EMAIL, PasswordHashed = "oldHash" };
            var updateDto = new UserUpdateNameOrMailOrPasswordDTO
            {
                Email = UserConstants.EMAIL,
                OldPassword = "oldPass",
                NewPassword = "newPass"
            };

            _mockUserRepo.Setup(r => r.GetByGuidAsync(userId)).ReturnsAsync(existingUser);
            _mockUserRepo.Setup(r => r.IsEmailAvailableAsync(UserConstants.EMAIL)).ReturnsAsync(true);
            _mockBCrypt.Setup(b => b.VerifyPasswordMatch("oldPass", "oldHash")).Returns(true);
            _mockBCrypt.Setup(b => b.HashPass("newPass",13)).Returns("newHash");
            _mockUserRepo.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(existingUser);

            // Act
            var result = await _userService.UpdateService(userId, updateDto);

            // Assert
            Assert.Equal(UserConstants.EMAIL, result.Email);
            Assert.IsType<UserViewDTO>(result);
            Assert.NotNull(result);

            _mockUserRepo.Verify(r => r.Update(It.Is<User>(u => u.Email == UserConstants.EMAIL && u.PasswordHashed == "newHash")), Times.Once);
            _mockUoW.Verify(u => u.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

    }
}
