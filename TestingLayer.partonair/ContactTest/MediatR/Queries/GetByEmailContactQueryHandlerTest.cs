using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Contacts;

using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Queries
{
    public class GetByEmailContactQueryHandlerTest : BaseContactApplicationMediatRTestFixture<GetByEmailContactQueryHandler>
    {
        private readonly ContactViewDTO _dto;
        public GetByEmailContactQueryHandlerTest()
        {
            _dto = new(
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
        }

        [Fact]
        public async Task GetByEmailContactQuery_ShouldReturn_Success_ICollection_ContactViewDto()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetByEmailAsyncService(It.IsAny<string>())).ReturnsAsync(_dto);

            // Act
            var result = await _handler.Handle(new GetByEmailContactQuery(It.IsAny<string>()), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContactViewDTO>(result);

            _mockContactService.Verify(v => v.GetByEmailAsyncService(It.IsAny<string>()),Times.Once);
        }

        [Fact]
        public async Task GetByEmailContactQuery_ShouldThrow_InfrastructureLayerException_EntityIsNullException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetByEmailAsyncService(It.IsAny<string>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new GetByEmailContactQuery(It.IsAny<string>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetByEmailAsyncService(It.IsAny<string>()), Times.Once);
        }

    }
}