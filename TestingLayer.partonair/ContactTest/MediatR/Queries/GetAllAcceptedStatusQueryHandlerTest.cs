using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Contacts;

using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Queries
{
    public class GetAllAcceptedStatusQueryHandlerTest : BaseContactApplicationMediatRTestFixture<GetAllAcceptedStatusQueryHandler>
    {
        private readonly ICollection<ContactViewDTO> _contactViews = [];
        private readonly ContactViewDTO _contact1;
        private readonly ContactViewDTO _contact2;
        public GetAllAcceptedStatusQueryHandlerTest()
        {
            _contact1 = new(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Jhon",
                "jhon@mail.be",
                DateTime.Now,
                true,
                false,
                null,
                DateTime.Now,
                StatusContact.Accepted.ToString()
                );

            _contact2 = new(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Jhon",
                "jhon@mail.be",
                DateTime.Now,
                true,
                false,
                null,
                DateTime.Now,
                StatusContact.Accepted.ToString()
                );

            _contactViews.Add(_contact1);
            _contactViews.Add(_contact2);
        }

        [Fact]
        public async Task GetAllAcceptedStatusQueryHandler_ShouldReturn_Success_ICollection_ContactViewDto()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetAllAcceptedStatusAsyncService(It.IsAny<string>()))
                               .ReturnsAsync(_contactViews);

            // Act
            var result = await _handler.Handle(new GetAllAcceptedStatusQuery(It.IsAny<string>()),CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ContactViewDTO>>(result);
            Assert.Equal(2,result.Count);

            _mockContactService.Verify(v => v.GetAllAcceptedStatusAsyncService(It.IsAny<string>()),Times.Once);
        }

        [Fact]
        public async Task GetAllAcceptedStatusQueryHandler_ShouldThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetAllAcceptedStatusAsyncService(It.IsAny<string>()))
                               .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _handler.Handle(new GetAllAcceptedStatusQuery(It.IsAny<string>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ApplicationLayerErrorType>(exception.ErrorType);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetAllAcceptedStatusAsyncService(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAcceptedStatusQueryHandler_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetAllAcceptedStatusAsyncService(It.IsAny<string>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new GetAllAcceptedStatusQuery(It.IsAny<string>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetAllAcceptedStatusAsyncService(It.IsAny<string>()), Times.Once);
        }
    }
}
