using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using Moq;
using System;


namespace TestingLayer.partonair.ContactTest.Services
{
    public class DeleteAsyncServiceTest : BaseContactApplicationServiceTestFixture
    {
        private readonly Guid _idSender;
        private readonly Guid _idContact;
        private readonly Contact _contact1;
        private readonly Contact _contact2;

        public DeleteAsyncServiceTest()
        {
            _idSender = Guid.NewGuid();
            _idContact = Guid.NewGuid();

            _contact1 = new Contact { Id = Guid.NewGuid() };
            _contact2 = new Contact { Id = Guid.NewGuid() };
        }

        [Fact]
        public async Task DeleteAsyncService_ShouldDeleteContacts_WhenBothContactsExist()
        {
            // Arrange
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idSender, _idContact)).ReturnsAsync(_contact1);
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idContact, _idSender)).ReturnsAsync(_contact2);

            // Act
            await _contactService.DeleteAsyncService(_idSender, _idContact);

            // Assert
            _mockUoW.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _mockContactRepo.Verify(repo => repo.Delete(_contact1.Id), Times.Once);
            _mockContactRepo.Verify(repo => repo.Delete(_contact2.Id), Times.Once);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
            _mockUoW.Verify(uow => uow.CommitTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsyncService_ShouldThrow_InfrastructureLayerException_EntityIsNullException()
        {
            // Arrange
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idSender, _idContact))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.EntityIsNullException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(()
                => _contactService.DeleteAsyncService(_idSender, _idContact));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.EntityIsNullException, exception.ErrorType);

            _mockUoW.Verify(uow => uow.RollbackTransactionAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsyncService_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idSender, _idContact)).ReturnsAsync(_contact1);
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idContact, _idSender)).ReturnsAsync(_contact2);
            _mockContactRepo.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() 
                => _contactService.DeleteAsyncService(_idSender, _idContact));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUoW.Verify(uow => uow.RollbackTransactionAsync(), Times.Once);
        }
    }
}
