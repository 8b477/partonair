using DomainLayer.partonair.Contracts;

using Moq;

namespace TestingLayer.partonair.User.Abstracts
{
    public abstract class UserBaseClassTest
    {
        protected readonly Mock<IUnitOfWork> _mockUoW;
        protected UserBaseClassTest()
        {
            _mockUoW = new();
        }
    }
}