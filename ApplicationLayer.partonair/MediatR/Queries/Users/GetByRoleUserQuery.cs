using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public record GetByRoleUserQuery(string Role) : IRequest<ICollection<UserViewDTO>>;
}
