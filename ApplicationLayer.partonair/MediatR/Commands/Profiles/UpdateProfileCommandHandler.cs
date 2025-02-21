using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Profiles
{
    public class UpdateProfileCommandHandler(IProfileService profileService) : IRequestHandler<UpdateProfileCommand, ProfileViewDTO>
    {
        private readonly IProfileService _profileService = profileService;
        public async Task<ProfileViewDTO> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            return await _profileService.UpdateService(request.Id, request.Profile);
        }
    }
}
