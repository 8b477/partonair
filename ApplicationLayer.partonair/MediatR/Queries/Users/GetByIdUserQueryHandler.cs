using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public class GetByIdUserQueryHandler(IUserService userService) : IRequestHandler<GetByIdUserQuery, UserViewDTO?>
    {
        private readonly IUserService _userService = userService;

        public async Task<UserViewDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsyncService(request.Id);
        }
    }

}
