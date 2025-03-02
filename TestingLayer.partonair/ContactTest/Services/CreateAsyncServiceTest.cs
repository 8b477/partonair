using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Exceptions;
using DomainLayer.partonair.Exceptions.Enums;

using Moq;


namespace TestingLayer.partonair.ContactTest.Services
{
    public class CreateAsyncServiceTest : BaseContactApplicationServiceTestFixture
    {
        private readonly Guid _idSender;
        private readonly Guid _idReceiver;
        private readonly ContactCreateDTO _contactCreateDto;
        private readonly Contact _contactDto;
        private readonly User _sender;
        private readonly User _receiver;
        public CreateAsyncServiceTest()
        {
            _idReceiver = Guid.NewGuid();
            _idSender = Guid.NewGuid();

            _contactCreateDto = new(_idSender,_idReceiver);

            _sender = new User { Id = _idSender };
            _receiver = new User { Id = _idReceiver };

            _contactDto = new Contact
            {

            };
        }


        [Fact]
        public async Task CreateAsyncService_ShouldCreateContact_WhenContactDoesNotExist()
        {
            // Arrange
            _mockUserRepo.Setup(repo => repo.GetByGuidAsync(_idSender)).ReturnsAsync(_sender);
            _mockUserRepo.Setup(repo => repo.GetByGuidAsync(_idReceiver)).ReturnsAsync(_receiver);
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idSender, _idReceiver))!.ReturnsAsync((Contact)null!);

            var contactEntity = new Contact { /* set properties */ };

            _mockContactRepo.Setup(repo => repo.CreateAsync(It.IsAny<Contact>())).ReturnsAsync(contactEntity);

            // Act
            var result = await _contactService.CreateAsyncService(_contactCreateDto);

            // Assert
            Assert.NotNull(result);
            _mockUoW.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _mockUoW.Verify(uow => uow.SaveChangesAsync(CancellationToken.None), Times.Once);
            _mockUoW.Verify(uow => uow.CommitTransactionAsync(), Times.Once);
            _mockContactRepo.Verify(repo => repo.CreateAsync(It.IsAny<Contact>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsyncService_ShouldThrow_ApplicationLayerException_ConstraintViolationErrorException()
        {
            // Arrange
            _mockUserRepo.Setup(repo => repo.GetByGuidAsync(_idSender)).ReturnsAsync(_sender);
            _mockUserRepo.Setup(repo => repo.GetByGuidAsync(_idReceiver)).ReturnsAsync(_receiver);
            _mockContactRepo.Setup(repo => repo.FindContactAsync(_idSender, _idReceiver))
                            .ThrowsAsync(new ApplicationLayerException(ApplicationLayerErrorType.ConstraintViolationErrorException));

            // Act
            var exception = await Assert.ThrowsAsync<ApplicationLayerException>(() => _contactService.CreateAsyncService(_contactCreateDto));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ApplicationLayerErrorType>(exception.ErrorType);
            Assert.Equal(ApplicationLayerErrorType.ConstraintViolationErrorException, exception.ErrorType);

            _mockUserRepo.Verify(repo => repo.GetByGuidAsync(_idSender),Times.Once);
            _mockUserRepo.Verify(repo => repo.GetByGuidAsync(_idReceiver),Times.Once);
            _mockUoW.Verify(uow => uow.RollbackTransactionAsync(),Times.Once);
        }

        [Fact]
        public async Task CreateAsyncService_ShouldThrow_InfrastructureLayerException_CancelationDatabaseException()
        {
            // Arrange
            _mockUserRepo.Setup(repo => repo.GetByGuidAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new InfrastructureLayerException(InfrastructureLayerErrorType.CancelationDatabaseException));

            // Act
            var exception = await Assert.ThrowsAsync<InfrastructureLayerException>(() 
                => _contactService.CreateAsyncService(_contactCreateDto));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<InfrastructureLayerErrorType>(exception.ErrorType);
            Assert.Equal(InfrastructureLayerErrorType.CancelationDatabaseException, exception.ErrorType);

            _mockUoW.Verify(uow => uow.RollbackTransactionAsync(), Times.Once);
        }

    }
}
