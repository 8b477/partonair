using DomainLayer.partonair.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfrastructureLayer.partonair.Persistence.Configuration
{
    internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            // Allows cast property 'Enum' into 'string'
            builder
                .Property(u => u.ContactStatus)
                .HasConversion<string>();

            builder
                .HasOne(c => c.Sender)
                .WithMany(u => u.RequestedContacts)
                .HasForeignKey(c => c.Id_Sender);

            builder
                .HasOne(c => c.Receiver)
                .WithMany(u => u.ReceivedContacts)
                .HasForeignKey(c => c.Id_Receiver);
        }
    }
}
