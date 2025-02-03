using DomainLayer.partonair.Enums;

using System.ComponentModel.DataAnnotations;


namespace DomainLayer.partonair.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHashed { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public DateTime UserCreatedAt { get; set; }
        public DateTime LastConnection { get; set; }
        public Roles Role { get; set; }

        // Navigation Property
        public Guid? FK_Profile { get; set; }
        public Profile? Profile { get; set; }
    }
}