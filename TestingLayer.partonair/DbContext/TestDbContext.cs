using InfrastructureLayer.partonair.Persistence;

using Microsoft.EntityFrameworkCore;

using TestingLayer.partonair.Entity;


namespace TestingLayer.partonair.DbContext
{
    public class TestDbContext(DbContextOptions<AppDbContext> options) : AppDbContext(options)
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TestEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
        }
    }

}
