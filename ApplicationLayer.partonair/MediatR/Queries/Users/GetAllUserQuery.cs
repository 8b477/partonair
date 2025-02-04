using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public record GetAllUserQuery() : IRequest<ICollection<UserViewDTO>>;
}
