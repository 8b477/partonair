using InfrastructureLayer.partonair.Persistence;
using InfrastructureLayer.partonair.Repositories;

using Microsoft.EntityFrameworkCore;

using TestingLayer.partonair.DbContext;
using TestingLayer.partonair.Entity;


namespace TestingLayer.partonair.GenericRepository
{
    public class BaseGenericRepositoryTestFixture : IDisposable
    {
        protected readonly TestDbContext _context;
        protected readonly GenericRepository<TestEntity> _repository;
        protected readonly List<TestEntity> _testData;

        public BaseGenericRepositoryTestFixture()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TestDbContext(options);

            _testData = new List<TestEntity>
        {
            new () { Id = Guid.NewGuid(), Name = "Test1" },
            new () { Id = Guid.NewGuid(), Name = "Test2" }
        };

            _context.TestEntities.AddRange(_testData);
            _context.SaveChanges();

            _repository = new FakeGenericRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
