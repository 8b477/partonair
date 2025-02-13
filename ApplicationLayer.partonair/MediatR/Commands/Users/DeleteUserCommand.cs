using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public record DeleteUserCommand(Guid Id) : IRequest;
}
