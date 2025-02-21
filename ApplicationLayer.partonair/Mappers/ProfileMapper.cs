using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;


namespace ApplicationLayer.partonair.Mappers
{
    public static class ProfileMapper
    {
        public static Profile ToEntity(this ProfileCreateDTO entity, User u)
        {
            return new Profile
            {
                ProfileDescription = entity.ProfileDescription,
                FK_User = u.Id,
                User = u
            };
        }

        public static ProfileViewDTO ToView(this Profile e)
        {
            return new ProfileViewDTO(e.Id,e.ProfileDescription,e.FK_User);
        } 

        public static void ToEntity(this ProfileUpdateDTO p, Profile e)
        {
            e.ProfileDescription = p.ProfilDescritpion;            
        }
    }
}
