using InfrastructureLayer.partonair.Persistence;
using InfrastructureLayer.partonair.Repositories;
using Microsoft.EntityFrameworkCore;


namespace TestingLayer.partonair.ProfilTest
{
    internal class BaseProfileInfrasctructureTestFixture : IDisposable
    {

        protected readonly AppDbContext _ctx;
        protected readonly ProfileRepository _profileRepo;

        protected BaseProfileInfrasctructureTestFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _ctx = new AppDbContext(options);
            _profileRepo = new (_ctx);
        }
        public void Dispose() => _ctx.Dispose();
    }
}
