using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public class GetUserByIdHandler(IUserService userService) : IRequestHandler<GetUserByIdQuery, UserViewDTO?>
    {
        private readonly IUserService _userService = userService;

        public async Task<UserViewDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByIdAsyncService(request.Id);
        }
    }

}
