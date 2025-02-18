using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Abstracts;
using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.Services
{
    public class UpdateServiceTest : ExtendUserServiceTest
    {
        private readonly Guid _id;
        private readonly User _existingUser;
        private readonly User _updatedUser;
        private readonly UserUpdateNameOrMailOrPasswordDTO _updateDto;

        public UpdateServiceTest() : base()
        {
            _id = Guid.NewGuid();
            _existingUser = CreateExistingUser(_id);
            _updateDto = CreateUpdateDto();
            _updatedUser = CreateUpdatedUser(_existingUser, _updateDto);
        }

        private static User CreateExistingUser(Guid id) => new User
        {
            Id = id,
            Email = "old@mail.be",
            PasswordHashed = "oldHash",
            UserName = "OldUserName",
            LastConnection = DateTime.Now.AddDays(-1),
            UserCreatedAt = DateTime.Now.AddDays(-30),
            Role = Roles.Visitor,
            IsPublic = false,
            FK_Profile = null
        };

        private static UserUpdateNameOrMailOrPasswordDTO CreateUpdateDto() => new()
        {
            UserName = "NewName",
            Email = "updat@mail.be",
            OldPassword = "oldPass",
            NewPassword = "newPass"
        };

        private static User CreateUpdatedUser(User existingUser, UserUpdateNameOrMailOrPasswordDTO updateDto) => new()
        {
            Id = existingUser.Id,
            Email = updateDto.Email ?? "",
            PasswordHashed = "newHash",
            UserName = updateDto.UserName ?? "",
            LastConnection = existingUser.LastConnection,
            UserCreatedAt = existingUser.UserCreatedAt,
            Role = existingUser.Role,
            IsPublic = existingUser.IsPublic,
            FK_Profile = existingUser.FK_Profile
        };

        private void SetupMocksForSuccessfulUpdate()
        {
            _mockUserRepo.Setup(r => r.GetByGuidAsync(It.IsAny<Guid>())).ReturnsAsync(_existingUser);
            _mockUserRepo.Setup(r => r.IsEmailAvailableAsync(_updateDto.Email!)).ReturnsAsync(true);
            _mockBCrypt.Setup(b => b.VerifyPasswordMatch(_updateDto.OldPassword!, _existingUser.PasswordHashed)).Returns(true);
            _mockBCrypt.Setup(b => b.HashPass(_updateDto.NewPassword!, 13)).Returns(_updatedUser.PasswordHashed);
            _mockUserRepo.Setup(r => r.Update(It.IsAny<User>())).ReturnsAsync(_updatedUser);
        }

        private void VerifyAllMockIsCalled()
        {
            _mockUserRepo.Verify(r => r.GetByGuidAsync(It.IsAny<Guid>()),Times.Once);
            _mockUserRepo.Verify(r => r.IsEmailAvailableAsync(_updateDto.Email!), Times.Once);
            _mockBCrypt.Verify(b => b.VerifyPasswordMatch(_updateDto.OldPassword!, _existingUser.PasswordHashed), Times.Once);
            _mockBCrypt.Verify(b => b.HashPass(_updateDto.NewPassword!, 13), Times.Once);
            _mockUserRepo.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateService_ShouldUpdateUser_Correctly()
        {
            // Arrange
            SetupMocksForSuccessfulUpdate();

            // Act
            var result = await _userService.UpdateService(It.IsAny<Guid>(), _updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);
            Assert.Equal(_existingUser.Id, result.Id);
            Assert.Equal(Roles.Visitor.ToString(), result.Role);
            Assert.Equal(_existingUser.Email, result.Email);
            Assert.NotEqual(_existingUser.UserName, result.UserName);
            Assert.False(result.IsPublic);
            Assert.Null(result.FK_Profile);

            VerifyAllMockIsCalled();
        }

        [Fact]
        public async Task UpdateService_ShouldThrowException_WhenEmailIsNotAvailable()
        {
            // Arrange
            var userDto = new UserUpdateNameOrMailOrPasswordDTO { Email = UserConstants.EMAIL };
            _mockUserRepo.Setup(repo => repo.IsEmailAvailableAsync(userDto.Email)).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(() => _userService.UpdateService(Guid.NewGuid(), userDto));
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.IsEmailAvailableAsync(userDto.Email),Times.Once);
        }

        [Fact]
        public async Task UpdateService_ShouldThrowExceptionWhenPasswordIsIncorrect()
        {
            // Arrange
            _mockUserRepo.Setup(r => r.GetByGuidAsync(It.IsAny<Guid>())).ReturnsAsync(_existingUser);
            _mockBCrypt.Setup(b => b.VerifyPasswordMatch(It.IsAny<string>(), It.IsAny<string>())).Throws(new ApplicationLayerException(ApplicationLayerErrorType.SaltParseBCryptException));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(() => _userService.UpdateService(It.Is<Guid>(p => p != Guid.Empty), _updateDto));
            Assert.Equal(ApplicationLayerErrorType.SaltParseBCryptException, exception.ErrorType);

            _mockUserRepo.Verify(v => v.GetByGuidAsync(It.IsAny<Guid>()), Times.Once);
            _mockBCrypt.Verify(v => v.VerifyPasswordMatch(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
