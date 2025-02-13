using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public class GetByEmailUserQueryHandler(IUserService userService) : IRequestHandler<GetByEmailUserQuery, UserViewDTO>
    {
        private readonly IUserService _userService = userService;
        public async Task<UserViewDTO> Handle(GetByEmailUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetByEmailAsyncService(request.Email);
        }
    }
}
