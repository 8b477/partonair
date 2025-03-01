using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public record DeleteContactCommand(Guid IdSender, Guid IdContact) : IRequest;
}
