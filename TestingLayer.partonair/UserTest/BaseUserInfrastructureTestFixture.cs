using InfrastructureLayer.partonair.Persistence;
using InfrastructureLayer.partonair.Repositories;

using Microsoft.EntityFrameworkCore;


namespace TestingLayer.partonair.UserTest
{
    public class BaseUserInfrastructureTestFixture : IDisposable
    {

        protected readonly AppDbContext _ctx;
        protected readonly UserRepository _userRepo;

        protected BaseUserInfrastructureTestFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _ctx = new AppDbContext(options);
            _userRepo = new UserRepository(_ctx);
        }
        public void Dispose() => _ctx.Dispose();
    }
}
