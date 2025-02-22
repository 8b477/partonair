using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Profiles;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ProfilTest.MediatR.Queries
{
    public class GetByIdProfileQueryHandlerTest(Guid idProfile) : BaseProfileApplicationTestFixture<GetByIdProfileQueryHandler>
    {
        private readonly Guid _idProfile = idProfile;    

        [Fact]
        public async Task GetByIdProfileQueryHandler_ShouldReturn_Success_ProfileViewDTO()
        { 
            // Act
            _mockProfileService.Setup(s => s.GetByGuidAsyncService(_idProfile)).ReturnsAsync(It.IsAny<ProfileViewDTO>());
            var result = await _handler.Handle(new GetByIdProfileQuery(_idProfile), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProfileViewDTO>(result);

            _mockProfileService.Verify(v => v.GetByGuidAsyncService(_idProfile),Times.Once);
        }

        [Fact]
        public async Task GetByIdProfileQueryHandler_ShouldThrow_Infrasctructure_ResourceNotFoundException()
        {
            // Act
            _mockProfileService.Setup(s => s.GetByGuidAsyncService(_idProfile)).ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                =>_handler.Handle(new GetByIdProfileQuery(_idProfile), CancellationToken.None));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockProfileService.Verify(v => v.GetByGuidAsyncService(_idProfile), Times.Once);
        }

    }
}