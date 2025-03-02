using ApplicationLayer.partonair.MediatR.Commands.Contacts;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

namespace TestingLayer.partonair.ContactTest.MediatR.Commands
{
    public class DeleteContactCommandHandlerTest : BaseContactApplicationMediatRTestFixture<DeleteContactCommandHandler>
    {
        private readonly Guid _idContact;
        private readonly Guid _idSender;
        public DeleteContactCommandHandlerTest()
        {
            _idContact = Guid.NewGuid();
            _idSender = Guid.NewGuid();
        }

        [Fact]
        public async Task DeleteContactCommandHandler_ShouldReturn_Success_Void()
        {
            // Arrange
            _mockContactService.Setup(s => s.DeleteAsyncService(It.IsAny<Guid>(), It.IsAny<Guid>()))
                               .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(new DeleteContactCommand(_idSender,_idContact), CancellationToken.None);

            // Assert
            _mockContactService.Verify(v => v.DeleteAsyncService(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteContactCommandHandler_ShouldThrow_InfrastructureLayerException_EntityIsNullException()
        {
            // Arrange
            _mockContactService.Setup(s => s.DeleteAsyncService(It.IsAny<Guid>(), It.IsAny<Guid>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                =>_handler.Handle(new DeleteContactCommand(_idSender, _idContact), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockContactService.Verify(v => v.DeleteAsyncService(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task DeleteContactCommandHandler_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.DeleteAsyncService(It.IsAny<Guid>(), It.IsAny<Guid>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new DeleteContactCommand(_idSender, _idContact), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.DeleteAsyncService(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once);
        }
    }
}