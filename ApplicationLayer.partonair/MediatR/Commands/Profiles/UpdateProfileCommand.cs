using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Profiles
{
    public record UpdateProfileCommand(Guid Id, ProfileUpdateDTO Profile) : IRequest<ProfileViewDTO>;
}
