using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Profiles
{
    public record DeleteProfileCommand(Guid Id) : IRequest;
}
