using DomainLayer.partonair.Contracts;

namespace DomainLayer.partonair.Entities
{
    public class Evaluation : IEntity
    {
        public Guid Id { get; set; }
        public string EvaluationCommentary { get; set; }
        public DateTime EvaluationCreatedAt { get; set; }
        public DateTime? EvaluationUpdatedAt { get; set; }
        public int EvaluationValue { get; set; }

        public Guid FK_Owner { get; set; }
        public User Owner { get; set; }
        public Guid FK_Sender { get; set; }
        public User Sender { get; set; }
    }
}