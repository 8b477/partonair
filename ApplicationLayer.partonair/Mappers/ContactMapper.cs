using ApplicationLayer.partonair.DTOs;

using DomainLayer.partonair.Entities;
using DomainLayer.partonair.Enums;


namespace ApplicationLayer.partonair.Mappers
{
    public static class ContactMapper
    {
        internal static Contact ToEntity(this ContactCreateDTO dto, User receiver, User sender)
        {
            return new Contact
            {
                Id_Receiver = receiver.Id, // User to add
                Id_Sender = dto.Id_Sender, // User Sender
                ContactEmail = receiver.Email,
                ContactName = receiver.UserName,
                AddedAt = DateTime.Now,
                Receiver = receiver,
                Sender = sender,
                ContactStatus = StatusContact.Pending
            };
        }

        internal static ContactViewDTO ToView(this Contact entity)
        {
            return new ContactViewDTO
            (
                entity.Id,
                entity.Id_Sender,
                entity.Id_Receiver,
                entity.ContactName,
                entity.ContactEmail,
                entity.AddedAt,
                entity.IsFriendly,
                entity.IsBlocked,
                entity.BlockedAt,
                entity.AcceptedAt,
                entity.ContactStatus.ToString()
            );
        }


    }
}
