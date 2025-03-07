﻿using ApplicationLayer.partonair.MediatR.Commands.Profiles;

using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;


namespace TestingLayer.partonair.ProfilTest.MediatR.Commands
{
    public class DeleteProfileCommandHandlerTest : BaseProfileApplicationMediatRTestFixture<DeleteProfileCommandHandler>
    {
        [Fact]
        public async Task DeleteProfileCommand_ShouldReturn_void()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _mockProfileService.Setup(s => s.DeleteAsyncService(id)).Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(new DeleteProfileCommand(id),CancellationToken.None);

            // Assert
            _mockProfileService.Verify(v => v.DeleteAsyncService(id),Times.Once);
        }

        [Fact]
        public async Task DeleteProfileCommand_ShouldThrow_InfrastuctureLayerExpection_CancelationDatabaseException()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _mockProfileService.Setup(s => s.DeleteAsyncService(id))
                .Throws(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _handler.Handle(new DeleteProfileCommand(id), CancellationToken.None));

            // Assert
            _mockProfileService.Verify(v => v.DeleteAsyncService(id), Times.Once);
        }

    }
}
