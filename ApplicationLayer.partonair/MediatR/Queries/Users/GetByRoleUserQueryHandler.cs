using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public class GetByRoleUserQueryHandler(IUserService userService) : IRequestHandler<GetByRoleUserQuery, ICollection<UserViewDTO>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ICollection<UserViewDTO>> Handle(GetByRoleUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByRoleAsyncService(request.Role);
        }
    }
}
