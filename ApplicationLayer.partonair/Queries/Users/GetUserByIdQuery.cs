using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.Queries.Users
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserViewDTO>;
}
