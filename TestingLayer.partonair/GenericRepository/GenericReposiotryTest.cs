using DomainLayer.partonair.Exceptions;

using TestingLayer.partonair.Entity;


namespace TestingLayer.partonair.GenericRepository
{
    public class GenericRepositoryTests : BaseGenericRepositoryTestFixture
    {
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            Assert.Equal(_testData.Count, result.Count);
            Assert.Equal(_testData.Select(x => x.Id), result.Select(x => x.Id));
        }

        [Fact]
        public async Task GetByGuidAsync_WithExistingId_ShouldReturnEntity()
        {
            // Arrange
            var expectedEntity = _testData.First();

            // Act
            var result = await _repository.GetByGuidAsync(expectedEntity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedEntity.Id, result.Id);
            Assert.Equal(expectedEntity.Name, result.Name);
        }

        [Fact]
        public async Task GetByGuidAsync_WithNonExistingId_ShouldThrowException()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<InfrastructureLayerException>(() =>
                _repository.GetByGuidAsync(nonExistingId));
        }

        [Fact]
        public async Task CreateAsync_ShouldAddNewEntity()
        {
            // Arrange
            var newEntity = new TestEntity
            {
                Id = Guid.NewGuid(),
                Name = "Test3"
            };

            // Act
            var result = await _repository.CreateAsync(newEntity);
            await _context.SaveChangesAsync();

            // Assert
            var savedEntity = await _context.TestEntities.FindAsync(newEntity.Id);
            Assert.NotNull(savedEntity);
            Assert.Equal(newEntity.Id, savedEntity.Id);
            Assert.Equal(newEntity.Name, savedEntity.Name);
        }

        [Fact]
        public async Task Update_ShouldModifyExistingEntity()
        {
            // Arrange
            var entityToUpdate = _testData.First();
            entityToUpdate.Name = "Updated Name";

            // Act
            var result = await _repository.Update(entityToUpdate);
            await _context.SaveChangesAsync();

            // Assert
            var updatedEntity = await _context.TestEntities.FindAsync(entityToUpdate.Id);
            Assert.Equal("Updated Name", updatedEntity.Name);
        }

        [Fact]
        public async Task Delete_ShouldRemoveEntity()
        {
            // Arrange
            var entityToDelete = _testData.First();

            // Act
            await _repository.Delete(entityToDelete.Id);
            await _context.SaveChangesAsync();

            // Assert
            var deletedEntity = await _context.TestEntities.FindAsync(entityToDelete.Id);
            Assert.Null(deletedEntity);
        }

    }
}