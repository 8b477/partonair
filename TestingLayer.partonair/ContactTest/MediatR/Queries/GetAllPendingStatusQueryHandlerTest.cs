using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Contacts;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions.Enums;

using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Queries
{
    public class GetAllPendingStatusQueryHandlerTest : BaseContactApplicationMediatRTestFixture<GetAllPendingStatusQueryHandler>
    {
        private readonly ICollection<ContactViewDTO> _contactViews = [];
        private readonly ContactViewDTO _contact1;
        private readonly ContactViewDTO _contact2;
        public GetAllPendingStatusQueryHandlerTest()
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
                StatusContact.Pending.ToString()
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
                StatusContact.Pending.ToString()
                );

            _contactViews.Add(_contact1);
            _contactViews.Add(_contact2);
        }


        [Fact]
        public async Task GetAllPendingStatusQueryHandler_ShouldReturn_Success_ICollection_ContactViewDto()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetAllPendingStatusAsyncService(It.IsAny<string>()))
                               .ReturnsAsync(_contactViews);

            // Act
            var result = await _handler.Handle(new GetAllPendingStatusQuery(It.IsAny<string>()), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ContactViewDTO>>(result);
            Assert.Equal(2, result.Count);

            _mockContactService.Verify(v => v.GetAllPendingStatusAsyncService(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetAllPendingStatusQueryHandler_ShouldThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetAllPendingStatusAsyncService(It.IsAny<string>()))
                               .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _handler.Handle(new GetAllPendingStatusQuery(It.IsAny<string>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ApplicationLayerErrorType>(exception.ErrorType);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetAllPendingStatusAsyncService(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetAllPendingStatusQueryHandler_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetAllPendingStatusAsyncService(It.IsAny<string>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new GetAllPendingStatusQuery(It.IsAny<string>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetAllPendingStatusAsyncService(It.IsAny<string>()), Times.Once);
        }
    }
}
