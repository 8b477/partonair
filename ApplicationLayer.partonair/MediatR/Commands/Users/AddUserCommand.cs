using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public record AddUserCommand(UserCreateDTO User) : IRequest<UserViewDTO>;
}
