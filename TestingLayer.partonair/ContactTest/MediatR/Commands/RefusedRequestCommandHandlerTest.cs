using ApplicationLayer.partonair.MediatR.Commands.Contacts;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;
using MediatR;

using Moq;

namespace TestingLayer.partonair.ContactTest.MediatR.Commands
{
    public class RefusedRequestCommandHandlerTest : BaseContactApplicationMediatRTestFixture<RefusedRequestCommandHandler>
    {
        private readonly string _expectedResponse;
        public RefusedRequestCommandHandlerTest()
        {
            _expectedResponse = "Success";
        }


        [Fact]
        public async Task RefusedRequestCommandHandler_ShoudReturn_Success_String()
        {
            // Arrange
            _mockContactService.Setup(s => s.RefusedRequestAsyncService(It.IsAny<Guid>()))
                               .ReturnsAsync(_expectedResponse);

            // Act
            var result = await _handler.Handle(new RefusedRequestCommand(It.IsAny<Guid>()),CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_expectedResponse, result);

            _mockContactService.Verify(v => v.RefusedRequestAsyncService(It.IsAny<Guid>()),Times.Once);
        }

        [Fact]
        public async Task LockContactRequestCommand_ShouldThrow_InfrastructureLayerException_ResourceNotFoundException()
        {
            // Arrange
            _mockContactService.Setup(s => s.RefusedRequestAsyncService(It.IsAny<Guid>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new RefusedRequestCommand(It.IsAny<Guid>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockContactService.Verify(v => v.RefusedRequestAsyncService(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task LockContactRequestCommand_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.RefusedRequestAsyncService(It.IsAny<Guid>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new RefusedRequestCommand(It.IsAny<Guid>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.RefusedRequestAsyncService(It.IsAny<Guid>()), Times.Once);
        }

    }
}
