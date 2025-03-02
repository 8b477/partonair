using ApplicationLayer.partonair.MediatR.Commands.Contacts;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;

namespace TestingLayer.partonair.ContactTest.MediatR.Commands
{
    public class AcceptedRequestContactHandlerTest : BaseContactApplicationMediatRTestFixture<AcceptedRequestContactCommandHandler>
    {
        [Fact]
        public async Task AcceptedRequestContactHandler_ShouldReturn_Success_string()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            string responseExpected = "Request accepted !";
            _mockContactService.Setup(s => s.AcceptedRequestAsyncService(id))
                               .ReturnsAsync(responseExpected);

            // Act
            var result = await _handler.Handle(new AcceptedRequestContactCommand(id), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Contains(responseExpected, result);

            _mockContactService.Verify(v => v.AcceptedRequestAsyncService(id), Times.Once());
        }

        [Fact]
        public async Task AcceptedRequestContactHandler_ShouldThrow_InfrastructureLayerException_ResourceNotFoundException()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            _mockContactService.Setup(s => s.AcceptedRequestAsyncService(id))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new AcceptedRequestContactCommand(id), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockContactService.Verify(v => v.AcceptedRequestAsyncService(id), Times.Once());
        }

        [Fact]
        public async Task AcceptedRequestContactHandler_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            Guid id = Guid.NewGuid();

            _mockContactService.Setup(s => s.AcceptedRequestAsyncService(id))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new AcceptedRequestContactCommand(id), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.AcceptedRequestAsyncService(id), Times.Once());
        }

    }
}