using DomainLayer.partonair.Entities;

using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer.partonair.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Allows cast property 'Enum' into 'string'
            modelBuilder.Entity<User>()
                        .Property(u => u.Role)
                        .HasConversion<string>(); 
        }
    }
}
