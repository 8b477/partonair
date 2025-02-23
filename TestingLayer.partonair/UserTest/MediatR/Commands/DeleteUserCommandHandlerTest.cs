using ApplicationLayer.partonair.MediatR.Commands.Users;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.UserTest.MediatR.Commands
{
    public class DeleteUserCommandHandlerTest : BaseUserApplicationTestFixture<DeleteUserCommandHandler>
    {
        [Fact]
        public async Task DeleteUserCommandHandler_ShouldReturnVoid()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserService.Setup(s => s.DeleteService(It.Is<Guid>(g => g != Guid.Empty)))
                .Verifiable();

            // Act
            await _handler.Handle(new DeleteUserCommand(userId), CancellationToken.None);

            // Assert
            _mockUserService.Verify(s => s.DeleteService(userId), Times.Once());
        }

        [Fact]
        public async Task DeleteUserCommandHandler_WhenOperationCanceled_ShouldThrowInfrastructureLayerException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUserService.Setup(s => s.DeleteService(It.IsAny<Guid>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(async () =>
                await _handler.Handle(new DeleteUserCommand(userId), CancellationToken.None));

            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);
        }


    }
}