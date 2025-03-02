using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Contacts;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Commands
{
    public class UnlockContactRequestCommandHandlerTest : BaseContactApplicationMediatRTestFixture<UnlockContactRequestCommandHandler>
    {
        private readonly string _expectedResponse;
        public UnlockContactRequestCommandHandlerTest()
        {
            _expectedResponse = "Success";
        }

        [Fact]
        public async Task UnlockContactRequestCommandHandler_ShouldReturn_Success_String()
        {
            // Arrange
            _mockContactService.Setup(s => s.UnlockContactRequestAsyncService(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()))
                .ReturnsAsync(_expectedResponse);

            // Act
            var result = await _handler.Handle(new UnlockContactRequestCommand(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()),CancellationToken.None);

            // Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.IsType<string>(result);

            _mockContactService.Verify(v => v.UnlockContactRequestAsyncService(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()),Times.Once);
        }

        [Fact]
        public async Task UnlockContactRequestCommandHandler_ShouldThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            _mockContactService.Setup(s => s.UnlockContactRequestAsyncService(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()))
                .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _handler.Handle(new UnlockContactRequestCommand(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ApplicationLayerErrorType>(exception.ErrorType);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockContactService.Verify(v => v.UnlockContactRequestAsyncService(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()), Times.Once);
        }

        [Fact]
        public async Task UnlockContactRequestCommandHandler_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.UnlockContactRequestAsyncService(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new UnlockContactRequestCommand(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.UnlockContactRequestAsyncService(It.IsAny<Guid>(), It.IsAny<UserToUnlock>()), Times.Once);
        }
    }
}
