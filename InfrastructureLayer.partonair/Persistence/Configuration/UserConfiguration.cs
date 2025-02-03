using DomainLayer.partonair.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InfrastructureLayer.partonair.Persistence.Configuration
{
    sealed internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Allows cast property 'Enum' into 'string'
            builder
                .Property(u => u.Role)
                .HasConversion<string>();

            builder
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<User>(p => p.FK_Profile)
                .IsRequired(false);
        }
    }
}
