using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserViewDTO>;
}
