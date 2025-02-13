using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public record GetByNameUserQuery(string Name) : IRequest<ICollection<UserViewDTO>>;
}
