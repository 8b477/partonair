using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Contacts;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Commands
{
    public class LockContactRequestCommandTest : BaseContactApplicationMediatRTestFixture<LockContactRequestCommandHandler>
    {
        private readonly Guid _idSender;
        private readonly UserToLock _dto;
        private readonly string _responseExpected;
        public LockContactRequestCommandTest()
        {
            _idSender = Guid.NewGuid();
            _dto = new(Guid.NewGuid());
            _responseExpected = "Success";
        }

        [Fact]
        public async Task LockContactRequestCommand_ShouldReturn_Success_String()
        {
            // Arrange
            _mockContactService.Setup(s => s.LockContactRequestAsyncService(_idSender, _dto))
                               .ReturnsAsync(_responseExpected);

            // Act
            var result = await _handler.Handle(new LockContactRequestCommand(_idSender, _dto), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<String>(result);

            _mockContactService.Verify(v => v.LockContactRequestAsyncService(_idSender, _dto));
        }

        [Fact]
        public async Task LockContactRequestCommand_ShouldThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            _mockContactService.Setup(s => s.LockContactRequestAsyncService(_idSender, _dto))
                               .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                =>_handler.Handle(new LockContactRequestCommand(_idSender, _dto), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockContactService.Verify(v => v.LockContactRequestAsyncService(_idSender, _dto));
        }

        [Fact]
        public async Task LockContactRequestCommand_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.LockContactRequestAsyncService(_idSender, _dto))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new LockContactRequestCommand(_idSender, _dto), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.LockContactRequestAsyncService(_idSender, _dto));
        }

    }
}