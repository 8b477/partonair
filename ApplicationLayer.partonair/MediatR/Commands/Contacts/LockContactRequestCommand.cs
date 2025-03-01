using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public record LockContactRequestCommand(Guid IdSender, UserToLock UserToLock) : IRequest<string>;
}
