using ApplicationLayer.partonair.Interfaces;
using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public class AddUserCommandHandler(IUserService userService) : IRequestHandler<AddUserCommand, UserViewDTO>
    {
        private readonly IUserService _userService = userService;

        public async Task<UserViewDTO> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.CreateAsync(request.User);
        }
    }
}
