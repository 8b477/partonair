using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;

using TestingLayer.partonair.UserTest.Constants;


namespace TestingLayer.partonair.UserTest.MediatR.Commands
{
    public class ChangeUserRoleCommandHandlerTest : BaseUserApplicationTestFixture
    {
        private readonly ChangeUserRoleCommandHandler _handler;
        public ChangeUserRoleCommandHandlerTest() => _handler = new ChangeUserRoleCommandHandler(_mockUserService.Object);


        [Fact]
        public async Task ChangeUserRoleCommandHandler_ShouldReturn_True()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var newUserRole = new UserChangeRoleDTO(UserConstants.ROLE_VISITOR);

            _mockUserService.Setup(s => s.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>())
                                  ).ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(new ChangeUserRoleCommand(id, newUserRole), CancellationToken.None);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);

            _mockUserService.Verify(s => s.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>()), Times.Once);
        }

        [Fact]
        public async Task ChangeUserRoleCommandHandler_ShouldReturn_False()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var newUserRole = new UserChangeRoleDTO(UserConstants.ROLE_VISITOR);

            _mockUserService.Setup(s => s.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>())
                                  ).ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(new ChangeUserRoleCommand(id, newUserRole), CancellationToken.None);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);

            _mockUserService.Verify(s => s.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>()), Times.Once);
        }

        [Fact]
        public async Task ChangeUserRoleCommandHandler_WhenUserIsNotFoundByIdentifierSupplied_ShouldThrow_InfrastructureEntityIsNullException()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var newUserRole = new UserChangeRoleDTO(UserConstants.ROLE_VISITOR);

            _mockUserService.Setup(s => s.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>()))
                           .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
            _handler.Handle(new ChangeUserRoleCommand(id, newUserRole), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockUserService.Verify(v => v.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>()), Times.Once);
        }

        [Fact]
        public async Task ChangeUserRoleCommandHandler_WhenTryToUpRoleUserOnVisitorAndThereIsNotAuthorize_ShouldThrow_ApplicationConstraintViolationErrorException()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var newUserRole = new UserChangeRoleDTO(UserConstants.ROLE_VISITOR);

            _mockUserService.Setup(s => s.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>()))
                           .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act && Assert
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(() =>
            _handler.Handle(new ChangeUserRoleCommand(id, newUserRole), CancellationToken.None));

            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockUserService.Verify(v => v.ChangeRoleAsyncService(It.Is<Guid>(p => p != Guid.Empty), It.IsAny<UserChangeRoleDTO>()), Times.Once);
        }

    }
}
