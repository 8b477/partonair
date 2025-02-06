using ApplicationLayer.partonair.Interfaces;

using ApplicationLayer.partonair.DTOs;


using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public class GetAllUserHandler(IUserService userService) : IRequestHandler<GetAllUserQuery, ICollection<UserViewDTO>>
    {
        private readonly IUserService _userService = userService;
        public async Task<ICollection<UserViewDTO>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetAllAsync();
        }
    }
}
