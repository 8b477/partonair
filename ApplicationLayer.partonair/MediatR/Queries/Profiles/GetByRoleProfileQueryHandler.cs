
using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Profiles
{
    public class GetByRoleProfileQueryHandler(IProfileService profileService) : IRequestHandler<GetByRoleProfileQuery, ICollection<ProfileAndUserViewDTO>>
    {
        private readonly IProfileService _profileService = profileService;
        public async Task<ICollection<ProfileAndUserViewDTO>> Handle(GetByRoleProfileQuery request, CancellationToken cancellationToken)
        {
            return await _profileService.GetByRoleAsyncService(request.Role);
        }
    }
}
