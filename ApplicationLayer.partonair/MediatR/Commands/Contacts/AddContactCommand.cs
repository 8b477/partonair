using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public record AddContactCommand(ContactCreateDTO Contact) : IRequest<ContactViewDTO>;
}
