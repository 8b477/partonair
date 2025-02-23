using ApplicationLayer.partonair.Services;

using DomainLayer.partonair.Contracts;

using Moq;


namespace TestingLayer.partonair.ProfilTest
{
    public class BaseProfileApplicationServiceTestFixture
    {
        protected readonly Mock<IProfileRepository> _mockProfileRepo;
        protected readonly Mock<IUserRepository> _mockUserRepo;
        protected readonly Mock<IUnitOfWork> _mockUnitOfWork;
        protected readonly ProfileService _profileService;

        public BaseProfileApplicationServiceTestFixture()
        {
            _mockUnitOfWork = new();
            _mockProfileRepo = new();
            _mockUserRepo = new();
            _profileService = new(_mockUnitOfWork.Object);

            _mockUnitOfWork.Setup(s => s.Profiles).Returns(_mockProfileRepo.Object);
            _mockUnitOfWork.Setup(s => s.Users).Returns(_mockUserRepo.Object);
        }
    }
}
