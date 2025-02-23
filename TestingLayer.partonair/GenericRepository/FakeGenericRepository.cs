using InfrastructureLayer.partonair.Persistence;
using InfrastructureLayer.partonair.Repositories;

using TestingLayer.partonair.Entity;


namespace TestingLayer.partonair.GenericRepository
{
    public class FakeGenericRepository(AppDbContext context) : GenericRepository<TestEntity>(context) { }
}
