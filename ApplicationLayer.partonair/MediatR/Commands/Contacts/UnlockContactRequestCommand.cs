using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public record UnlockContactRequestCommand(Guid IdSender, UserToUnlock UserToUnLock) : IRequest<string>;
}
