
namespace DomainLayer.partonair.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string ProfilDescription { get; set; } = string.Empty;

        // Prop nav
        public Guid FK_User { get; set; }
        public User User { get; set; } = new();
    }
}
