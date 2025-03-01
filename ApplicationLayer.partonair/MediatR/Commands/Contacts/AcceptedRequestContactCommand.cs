using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public record AcceptedRequestContactCommand(Guid IdContact) : IRequest<string>;
}
