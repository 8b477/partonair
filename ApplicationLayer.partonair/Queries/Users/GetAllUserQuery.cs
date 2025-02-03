using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.Queries.Users
{
    public record GetAllUserQuery() : IRequest<ICollection<UserViewDTO>>;
}
