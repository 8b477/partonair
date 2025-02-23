using DomainLayer.partonair.Contracts;


namespace TestingLayer.partonair.Entity
{
    public class TestEntity : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
