
using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, UserViewDTO>
    {
        private readonly IUserService _userService;
        public Task<UserViewDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
