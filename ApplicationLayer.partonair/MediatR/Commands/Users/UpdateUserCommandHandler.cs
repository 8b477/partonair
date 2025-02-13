using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, UserViewDTO>
    {
        private readonly IUserService _userService = userService;
        public async Task<UserViewDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateService(request.Id, request.User);
        }
    }
}
