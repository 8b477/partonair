using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.Services;

using DomainLayer.partonair.Contracts;

using Microsoft.Extensions.Logging;

using Moq;


namespace TestingLayer.partonair.UserTest
{
    public class ExtendBaseUserApplicationServiceTestFixture
    {
        protected readonly Mock<IUnitOfWork> _mockUoW;
        protected readonly Mock<IUserRepository> _mockUserRepo;
        protected readonly Mock<IBCryptService> _mockBCrypt;
        protected readonly Mock<ILogger<UserService>> _mockLogger;
        protected readonly UserService _userService;

        public ExtendBaseUserApplicationServiceTestFixture()
        {
            _mockUoW = new();
            _mockUserRepo = new();
            _mockBCrypt = new();
            _mockLogger = new();
            _mockUoW.Setup(uow => uow.Users).Returns(_mockUserRepo.Object);
            _userService = new UserService(_mockUoW.Object, _mockBCrypt.Object, _mockLogger.Object);

        }
    }
}
