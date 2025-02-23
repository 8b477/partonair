using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Queries.Profiles;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ProfilTest.MediatR.Queries
{
    public class GetByRoleProfileQueryHandlerTest : BaseProfileApplicationMediatRTestFixture<GetByRoleProfileQueryHandler>
    {
        [Fact]
        public async Task GetByRoleProfileQueryHandler_ShouldReturn_Success_CollectionProfileAndUserViewDTO()
        {
            // Arrange
            string role = "Visitor";
            UserViewDTO user1 = new(Guid.NewGuid(),"user1","user1@mail.be",false,DateTime.Now,DateTime.Now, role, null);
            UserViewDTO user2 = new(Guid.NewGuid(),"user2","user2@mail.be",false,DateTime.Now,DateTime.Now, role, null);
            ProfileViewDTO profile1 = new(Guid.NewGuid(), "First  description", user1.Id);
            ProfileViewDTO profile2 = new(Guid.NewGuid(), "Second  description", user2.Id);
            ICollection<ProfileAndUserViewDTO> users = 
                [
                    new (user1,profile1),
                    new (user2,profile2)
                ];

            _mockProfileService.Setup(s => s.GetByRoleAsyncService(role)).ReturnsAsync(users);

            // Act
            var result = await _handler.Handle(new GetByRoleProfileQuery(role),CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<ProfileAndUserViewDTO>>(result);

            _mockProfileService.Verify(v => v.GetByRoleAsyncService(role),Times.Once);
        }

        [Fact]
        public async Task GetByRoleProfileQueryHandler_ShouldThrow_ApplicationLayerErrorType_ConstraintViolationErrorException()
        {
            // Arrange
            string role = "badRole";


            _mockProfileService.Setup(s => s.GetByRoleAsyncService(role))
                .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                =>_handler.Handle(new GetByRoleProfileQuery(role), CancellationToken.None));

            // Assert
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockProfileService.Verify(v => v.GetByRoleAsyncService(role), Times.Once);
        }

        [Fact]
        public async Task GetByRoleProfileQueryHandler_ShouldThrow_InfrastructureLayerErrorType_ResourceNotFoundException()
        {
            // Arrange
            string role = "";


            _mockProfileService.Setup(s => s.GetByRoleAsyncService(role))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new GetByRoleProfileQuery(role), CancellationToken.None));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockProfileService.Verify(v => v.GetByRoleAsyncService(role), Times.Once);
        }

    }
}
