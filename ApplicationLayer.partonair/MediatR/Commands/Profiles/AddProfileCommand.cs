using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Profiles
{
    public record AddProfileCommand(Guid IdUser, ProfileCreateDTO Entity) : IRequest<ProfileViewDTO>;
}