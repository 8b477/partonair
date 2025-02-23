using ApplicationLayer.partonair.Interfaces;

using Moq;


namespace TestingLayer.partonair.UserTest
{
    public class BaseUserApplicationTestFixture<THandler> where THandler : class
    {
        protected readonly Mock<IUserService> _mockUserService;
        protected readonly THandler _handler;
        protected BaseUserApplicationTestFixture()
        {
            _mockUserService = new();
            _handler = (THandler)Activator.CreateInstance(typeof(THandler), _mockUserService.Object)!;
        }
    }
}