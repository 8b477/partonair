using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Contacts;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions.Enums;

using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Queries
{
    public class GetByNameContactQueryHandlerTest : BaseContactApplicationMediatRTestFixture<GetByNameContactQueryHandler>
    {
        private readonly ICollection<ContactViewDTO> _contactViews = [];
        private readonly ContactViewDTO _contact1;
        private readonly ContactViewDTO _contact2;
        public GetByNameContactQueryHandlerTest()
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
        public async Task GetByNameContactQueryHandler_ShouldReturn_Success_ICollection_ContactViewDto()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetByNameAsyncService(It.IsAny<string>())).ReturnsAsync(_contactViews);

            // Act
            var result = await _handler.Handle(new GetByNameContactQuery(It.IsAny<string>()), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<ContactViewDTO>>(result);
            Assert.Equal(2,result.Count);

            _mockContactService.Verify(v => v.GetByNameAsyncService(It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public async Task GetByNameContactQueryHandler_ShouldThrow_InfrastructureLayerException_EntityIsNullException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetByNameAsyncService(It.IsAny<string>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new GetByNameContactQuery(It.IsAny<string>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetByNameAsyncService(It.IsAny<string>()), Times.Once);
        }
    }
}
