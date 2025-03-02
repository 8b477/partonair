using ApplicationLayer.partonair.Interfaces;
using Moq;


namespace TestingLayer.partonair.ContactTest
{
    public class BaseContactApplicationMediatRTestFixture<Thandler> where Thandler : class
    {
        protected readonly Mock<IContactService> _mockContactService;
        protected readonly Thandler _handler;
        public BaseContactApplicationMediatRTestFixture()
        {
            _mockContactService = new();
            _handler = (Thandler)Activator.CreateInstance(typeof(Thandler), _mockContactService.Object)!;
        }
    }
}
