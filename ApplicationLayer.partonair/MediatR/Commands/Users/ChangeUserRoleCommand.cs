using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Users
{
    public record ChangeUserRoleCommand(Guid id, UserChangeRoleDTO NewRole) : IRequest<bool>;
}
