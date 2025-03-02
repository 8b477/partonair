using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Contacts;

using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Commands
{
    public class AddContactCommandHandlerTest : BaseContactApplicationMediatRTestFixture<AddContactCommandHandler>
    {
        private readonly Guid _idSender;
        private readonly Guid _idReceiver;
        private readonly Guid _idContact;
        private readonly ContactCreateDTO _dto;
        private readonly ContactViewDTO _viewDto;
        public AddContactCommandHandlerTest()
        {
            _idSender = Guid.NewGuid();
            _idReceiver = Guid.NewGuid();
            _idContact = Guid.NewGuid();
            _dto = new(_idSender, _idReceiver);
            _viewDto = new
                (
                _idContact,
                _idSender,
                _idReceiver,
                "Jhon",
                "jhon@mail.be",
                DateTime.Now,
                true,
                false,
                null,
                DateTime.Now,
                StatusContact.Accepted.ToString()
                );
        }


        [Fact]
        public async Task AddContactCommandHandler_ShouldReturn_Success_ContactViewDTO()
        {
            // Arrange
            _mockContactService.Setup(s => s.CreateAsyncService(_dto)).ReturnsAsync(_viewDto);

            // Act
            var result = await _handler.Handle(new AddContactCommand(_dto),CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContactViewDTO>(result);

            _mockContactService.Verify(v => v.CreateAsyncService(_dto),Times.Once);
        }

        [Fact]
        public async Task AddContactCommandHandler_ShouldThrow_ApplicationLayerExpection_ConstraintViolationErrorException()
        {
            // Arrange
            _mockContactService.Setup(s => s.CreateAsyncService(_dto))
                .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _handler.Handle(new AddContactCommand(_dto), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockContactService.Verify(v => v.CreateAsyncService(_dto), Times.Once);
        }

        [Fact]
        public async Task AddContactCommandHandler_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.CreateAsyncService(_dto))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new AddContactCommand(_dto), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.CreateAsyncService(_dto), Times.Once);
        }

    }
}