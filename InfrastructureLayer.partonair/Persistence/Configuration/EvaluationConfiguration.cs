using DomainLayer.partonair.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace InfrastructureLayer.partonair.Persistence.Configuration
{
    internal class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder
                    .HasOne(e => e.Owner)
                    .WithMany(u => u.ReceivedEvaluations)
                    .HasForeignKey(e => e.FK_Owner);

            builder
                    .HasOne(e => e.Sender)
                    .WithMany(u => u.RequestedEvaluations)
                    .HasForeignKey(e => e.FK_Sender);
        }
    }
}
