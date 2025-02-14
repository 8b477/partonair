using DomainLayer.partonair.Contracts;

namespace DomainLayer.partonair.Entities
{
    public class Profile : IEntity
    {
        public Guid Id { get; set; }
        public string ProfileDescription { get; set; } = string.Empty;

        // Prop nav
        public Guid FK_User { get; set; }
        public User User { get; set; } = new();
    }
}
