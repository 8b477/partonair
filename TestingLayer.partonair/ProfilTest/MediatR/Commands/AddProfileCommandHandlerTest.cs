using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.MediatR.Commands.Profiles;

using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ProfilTest.MediatR.Commands
{
    public class AddProfileCommandHandlerTest : BaseProfileApplicationTestFixture<AddProfileCommandHandler>
    {
        [Fact]
        public async Task AddProfileCommandHandler_ShouldReturn_ProfileViewDTO()
        {
            // Arrange
            Guid idUser = Guid.NewGuid();
            Guid idProfil = Guid.NewGuid();
            ProfileCreateDTO dto = new("description testing");
            ProfileViewDTO view = new(idProfil, dto.ProfileDescription, idUser);
            // Act
            _mockProfileService.Setup(s => s.CreateAsyncService(idUser, dto))
                               .ReturnsAsync(view);

            var result = await _handler.Handle(new AddProfileCommand(idUser, dto), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ProfileViewDTO>(result);
            Assert.Equal(dto.ProfileDescription, result.ProfilDescritpion);

            _mockProfileService.Verify(v => v.CreateAsyncService(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ProfileCreateDTO>())
                   ,Times.Once);
        }

        [Fact]
        public async Task AddProfileCommandHandler_ShouldThrow_ApplicationLayerErrorType_ConstraintViolationErrorException()
        {
            // Arrange
            Guid idUser = Guid.NewGuid();
            ProfileCreateDTO dto = new("description testing");

            // Act
            _mockProfileService.Setup(s => s.CreateAsyncService(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ProfileCreateDTO>()))
                               .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            var exception =  await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _handler.Handle(new AddProfileCommand(idUser, dto), CancellationToken.None));

            // Assert
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockProfileService.Verify(v => v.CreateAsyncService(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ProfileCreateDTO>())
                   , Times.Once);
        }

        [Fact]
        public async Task AddProfileCommandHandler_ShouldThrow_InfrastructureLayerErrorType_ResourceNotFoundException()
        {
            // Arrange
            Guid idUser = Guid.NewGuid();
            ProfileCreateDTO dto = new("description testing");

            // Act
            _mockProfileService.Setup(s => s.CreateAsyncService(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ProfileCreateDTO>()))
                               .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.ResourceNotFoundException));

            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new AddProfileCommand(idUser, dto), CancellationToken.None));

            // Assert
            Assert.Equal(InfrastructureLayerErrorType.ResourceNotFoundException, exception.ErrorType);

            _mockProfileService.Verify(v => v.CreateAsyncService(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ProfileCreateDTO>())
                   , Times.Once);
        }

    }

}