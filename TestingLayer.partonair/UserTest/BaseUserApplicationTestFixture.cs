using ApplicationLayer.partonair.Interfaces;

using DomainLayer.partonair.Contracts;

using Moq;


namespace TestingLayer.partonair.UserTest
{
    public class BaseUserApplicationTestFixture
    {
        protected readonly Mock<IUnitOfWork> _mockUoW;
        protected readonly Mock<IUserService> _mockUserService;
        protected BaseUserApplicationTestFixture()
        {
            _mockUoW = new();
            _mockUserService = new();
        }
    }
}