using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserService _userService = userService;
        public Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.DeleteService(request.Id);
        }
    }
}
