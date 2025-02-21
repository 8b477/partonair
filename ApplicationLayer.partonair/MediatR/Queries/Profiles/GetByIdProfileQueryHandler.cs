using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Profiles
{
    public class GetByIdProfileQueryHandler(IProfileService profileService) : IRequestHandler<GetByIdProfileQuery, ProfileViewDTO>
    {
        private readonly IProfileService _profileService = profileService;
        public async Task<ProfileViewDTO> Handle(GetByIdProfileQuery request, CancellationToken cancellationToken)
        {
            return await _profileService.GetByGuidAsyncService(request.IdUser);
        }
    }
}
