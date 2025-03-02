using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Contacts;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Enums;

using Moq;


namespace TestingLayer.partonair.ContactTest.MediatR.Queries
{
    public class GetByGuidContactQueryHandlerTest : BaseContactApplicationMediatRTestFixture<GetByGuidContactQueryHandler>
    {
        private readonly ContactViewDTO _dto;
        public GetByGuidContactQueryHandlerTest()
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
        public async Task GetByGuidContactQueryHandler_ShouldReturn_Success_ContactViewDto()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetByGuidAsyncService(It.IsAny<Guid>())).ReturnsAsync(_dto);

            // Act
            var result = await _handler.Handle(new GetByGuidContactQuery(It.IsAny<Guid>()), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ContactViewDTO>(result);

            _mockContactService.Verify(v => v.GetByGuidAsyncService(It.IsAny<Guid>()), Times.Once);
        }


        [Fact]
        public async Task GetByGuidContactQueryHandler_ShouldThrow_InfrastructureLayerException_ResourceNotFoundException()
        {
            // Arrange
            _mockContactService.Setup(s => s.GetByGuidAsyncService(It.IsAny<Guid>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new GetByGuidContactQuery(It.IsAny<Guid>()), CancellationToken.None));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockContactService.Verify(v => v.GetByGuidAsyncService(It.IsAny<Guid>()), Times.Once);
        }
    }
}
