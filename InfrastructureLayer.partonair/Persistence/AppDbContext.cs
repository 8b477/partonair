using DomainLayer.partonair.Entities;

using InfrastructureLayer.partonair.Persistence.Configuration;

using Microsoft.EntityFrameworkCore;


namespace InfrastructureLayer.partonair.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users {get; set;}
        public DbSet<Profile> Profiles {get; set;}
        public DbSet<Contact> Contacts {get; set;}
        public DbSet<Evaluation> Evaluations {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            modelBuilder.ApplyConfiguration(new EvaluationConfiguration());
        }
    }
}
