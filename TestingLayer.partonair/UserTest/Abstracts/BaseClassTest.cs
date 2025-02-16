using ApplicationLayer.partonair.Interfaces;

using DomainLayer.partonair.Contracts;

using Moq;

namespace TestingLayer.partonair.UserTest.Abstracts
{
    public abstract class UserBaseClassTest
    {
        protected readonly Mock<IUnitOfWork> _mockUoW;
        protected readonly Mock<IUserService> _mockUserService;
        protected UserBaseClassTest()
        {
            _mockUoW = new();
            _mockUserService = new();
        }
    }
}