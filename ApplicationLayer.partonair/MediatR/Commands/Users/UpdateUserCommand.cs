using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public record UpdateUserCommand(UserUpdateDTO User) : IRequest<UserViewDTO>;
}
