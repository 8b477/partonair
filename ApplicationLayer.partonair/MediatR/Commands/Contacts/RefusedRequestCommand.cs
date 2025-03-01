using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public record RefusedRequestCommand(Guid IdContact) : IRequest<string>;
}