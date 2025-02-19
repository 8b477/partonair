using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Commands
{
    public class UpdateUserCommandHandlerTest : BaseUserApplicationTestFixture
    {
        private readonly UpdateUserCommandHandler _handler;
        private readonly UserUpdateNameOrMailOrPasswordDTO _userToUpdate;
        public UpdateUserCommandHandlerTest()
        {
            _handler = new UpdateUserCommandHandler(_mockUserService.Object);
            _userToUpdate = new UserUpdateNameOrMailOrPasswordDTO
            {
                UserName = UserConstants.NAME,
                Email = UserConstants.EMAIL
            };
        }

        [Fact]
        public async Task UpdateUserCommandHandler_ShouldReturn_UserViewDTO()
        {
            // Arrange
            var expectedUser = new UserViewDTO
                (
                    new Guid(),
                   UserConstants.NAME,
                   UserConstants.EMAIL,
                   false,
                   DateTime.Now,
                   DateTime.Now,
                   UserConstants.ROLE_VISITOR,
                   null
                );

            _mockUserService.Setup(s => s.UpdateService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserUpdateNameOrMailOrPasswordDTO>()))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _handler.Handle(new UpdateUserCommand(Guid.NewGuid(), _userToUpdate), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserViewDTO>(result);

            _mockUserService.Verify(v => v.UpdateService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserUpdateNameOrMailOrPasswordDTO>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_WhenUserIsNotFoundById_ShouldThrow_InfrastructureResourceNotFoundException()
        {
            // Arrange
            _mockUserService.Setup(s => s.UpdateService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserUpdateNameOrMailOrPasswordDTO>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new UpdateUserCommand(Guid.NewGuid(), _userToUpdate), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockUserService.Verify(v => v.UpdateService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserUpdateNameOrMailOrPasswordDTO>()), Times.Once);
        }

        [Fact]
        public async Task UpdateUserCommandHandler_WhenDatabaseOperationCanceled_ShouldThrow_InfrastructureOperationCanceledException()
        {
            // Arrange
            _mockUserService.Setup(s => s.UpdateService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserUpdateNameOrMailOrPasswordDTO>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _handler.Handle(new UpdateUserCommand(Guid.NewGuid(), _userToUpdate), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUserService.Verify(v => v.UpdateService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserUpdateNameOrMailOrPasswordDTO>()), Times.Once);
        }

    }
}