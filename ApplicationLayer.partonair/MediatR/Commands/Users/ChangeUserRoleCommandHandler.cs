using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public class ChangeUserRoleCommandHandler(IUserService userService) : IRequestHandler<ChangeUserRoleCommand, bool>
    {
        private readonly IUserService _userService = userService;
        public async Task<bool> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ChangeRoleAsyncService(request.id, request.NewRole);
        }
    }
}
