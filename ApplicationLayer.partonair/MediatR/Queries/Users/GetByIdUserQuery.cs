using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public record GetByIdUserQuery(Guid Id) : IRequest<UserViewDTO>;
}
