using ApplicationLayer.partonair.Services;

using DomainLayer.partonair.Contracts;

using Moq;


namespace TestingLayer.partonair.ContactTest
{
    public class BaseContactApplicationServiceTestFixture
    {
        protected readonly Mock<IUnitOfWork> _mockUoW;
        protected readonly Mock<IContactRepository> _mockContactRepo;
        protected readonly Mock<IUserRepository> _mockUserRepo;
        protected readonly ContactService _contactService;

        public BaseContactApplicationServiceTestFixture()
        {
            _mockUoW = new();
            _mockContactRepo = new();
            _mockUserRepo = new();
            _mockUoW.Setup(uow => uow.Users).Returns(_mockUserRepo.Object);
            _mockUoW.Setup(uow => uow.Contacts).Returns(_mockContactRepo.Object);
            _contactService = new ContactService(_mockUoW.Object);
        }
    }
}
