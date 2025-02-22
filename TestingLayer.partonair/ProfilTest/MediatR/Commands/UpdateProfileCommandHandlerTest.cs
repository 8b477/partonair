using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Profiles;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ProfilTest.MediatR.Commands
{
    public class UpdateProfileCommandHandlerTest : BaseProfileApplicationTestFixture<UpdateProfileCommandHandler>
    {
        [Fact]
        public async Task UpdateProfileCommandHandler_ShouldReturn_Success_ProfileViewDTO()
        {
            // Arrange
            Guid idProfile = Guid.NewGuid();
            Guid idUser = Guid.NewGuid();
            string profileDescription = "ProfileDescription updating test";
            var dto = new ProfileUpdateDTO(profileDescription);
            var view = new ProfileViewDTO(idProfile, profileDescription, idUser);

            // Act
            _mockProfileService.Setup(s => s.UpdateService(idProfile, dto)).ReturnsAsync(view);
            var result = await _handler.Handle(new UpdateProfileCommand(idProfile, dto), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProfileViewDTO>(result);

            _mockProfileService.Verify(v => v.UpdateService(idProfile, dto),Times.Once);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_ShouldThrow_InfrastuctureLayerException_UpdateDatabaseException()
        {
            // Arrange
            Guid idProfile = Guid.NewGuid();
            Guid idUser = Guid.NewGuid();
            string profileDescription = "ProfileDescription updating test";
            var dto = new ProfileUpdateDTO(profileDescription);
            var view = new ProfileViewDTO(idProfile, profileDescription, idUser);

            // Act
            _mockProfileService.Setup(s => s.UpdateService(idProfile, dto))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.UpdateDatabaseException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() 
                => _handler.Handle(new UpdateProfileCommand(idProfile, dto), CancellationToken.None));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.UpdateDatabaseException, exception.ErrorType);

            _mockProfileService.Verify(v => v.UpdateService(idProfile, dto), Times.Once);
        }

        [Fact]
        public async Task UpdateProfileCommandHandler_ShouldThrow_InfrastuctureLayerException_ConcurrencyDatabaseException()
        {
            // Arrange
            Guid idProfile = Guid.NewGuid();
            Guid idUser = Guid.NewGuid();
            string profileDescription = "ProfileDescription updating test";
            var dto = new ProfileUpdateDTO(profileDescription);
            var view = new ProfileViewDTO(idProfile, profileDescription, idUser);

            // Act
            _mockProfileService.Setup(s => s.UpdateService(idProfile, dto))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ConcurrencyDatabaseException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new UpdateProfileCommand(idProfile, dto), CancellationToken.None));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ConcurrencyDatabaseException, exception.ErrorType);

            _mockProfileService.Verify(v => v.UpdateService(idProfile, dto), Times.Once);
        }

    }
}
