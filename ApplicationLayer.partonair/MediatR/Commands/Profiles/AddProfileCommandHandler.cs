using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using DomainLayer.partonair.Entities;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Profiles
{
    public class AddProfileCommandHandler(IProfileService profileService) : IRequestHandler<AddProfileCommand, ProfileViewDTO>
    {
        private readonly IProfileService _profileService = profileService;
        public async Task<ProfileViewDTO> Handle(AddProfileCommand request, CancellationToken cancellationToken)
        {
            return await _profileService.CreateAsyncService(request.IdUser, request.Entity);
        }
    }
}
