using DomainLayer.partonair.Contracts;
using DomainLayer.partonair.Enums;

using System.ComponentModel.DataAnnotations;


namespace DomainLayer.partonair.Entities
{
    public class User : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHashed { get; set; }
        public bool IsPublic { get; set; }
        public DateTime UserCreatedAt { get; set; }
        public DateTime LastConnection { get; set; }
        public Roles Role { get; set; }

        public Guid? FK_Profile { get; set; }
        public Profile? Profile { get; set; }

        public ICollection<Contact> RequestedContacts { get; set; } = [];
        public ICollection<Contact> ReceivedContacts { get; set; } = [];
        public ICollection<Evaluation> RequestedEvaluations { get; set; } = [];
        public ICollection<Evaluation> ReceivedEvaluations { get; set; } = [];
    }
}