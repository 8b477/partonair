using ApplicationLayer.partonair.Interfaces;

using Moq;


namespace TestingLayer.partonair.ProfilTest
{
    public class BaseProfileApplicationMediatRTestFixture<Thandler> 
        where Thandler : class
    {
        protected readonly Mock<IProfileService> _mockProfileService;
        protected readonly Thandler _handler;
        public BaseProfileApplicationMediatRTestFixture()
        {
            _mockProfileService = new();
            _handler = (Thandler)Activator.CreateInstance(typeof(Thandler), _mockProfileService.Object)!;
        }
    }
}
