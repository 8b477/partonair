using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;
using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions;

using MediatR;

using Moq;


namespace TestingLayer.partonair.ContactTest.Services
{
    public class AcceptedRequestAsyncServiceTest : BaseContactApplicationServiceTestFixture
    {
        private readonly Guid _idContact;
        private readonly Contact _existingContact;

        public AcceptedRequestAsyncServiceTest()
        {
            _idContact = Guid.NewGuid();
            _existingContact = new Contact
            {
                Id = _idContact,
                IsFriendly = false,
                ContactStatus = StatusContact.Pending,
                AcceptedAt = null
            };
        }

        [Fact]
        public async Task AcceptedRequestAsyncService_ShouldUpdateContact_WhenContactExists()
        {
            // Arrange
            _mockContactRepo.Setup(repo => repo.GetByGuidAsync(_idContact)).ReturnsAsync(_existingContact);

            // Act
            var result = await _contactService.AcceptedRequestAsyncService(_idContact);

            // Assert
            Assert.Equal("Request accepted !", result);
            Assert.True(_existingContact.IsFriendly);
            Assert.Equal(StatusContact.Accepted, _existingContact.ContactStatus);
            Assert.NotNull(_existingContact.AcceptedAt);

            _mockContactRepo.Verify(repo => repo.GetByGuidAsync(_idContact), Times.Once);
            _mockContactRepo.Verify(repo => repo.Update(_existingContact), Times.Once);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task AcceptedRequestAsyncService_ShouldThrow_WhenContactNotFound()
        {
            // Arrange
            _mockContactRepo.Setup(repo => repo.GetByGuidAsync(_idContact))
                            .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(()
                => _contactService.AcceptedRequestAsyncService(_idContact));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ApplicationLayerErrorType>(exception.ErrorType);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockContactRepo.Verify(repo => repo.GetByGuidAsync(_idContact), Times.Once);
            _mockContactRepo.Verify(repo => repo.Update(It.IsAny<Contact>()), Times.Never);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Never);
        }

    }
}
