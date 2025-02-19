using DomainLayer.partonair.Entities;

using InfrastructureLayer.partonair.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;


namespace InfrastructureLayer.partonair.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Profile> Profiles {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
