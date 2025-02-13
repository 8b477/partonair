using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public record UpdateUserCommand(Guid Id, UserUpdateNameOrMailOrPasswordDTO User) : IRequest<UserViewDTO>;
}
