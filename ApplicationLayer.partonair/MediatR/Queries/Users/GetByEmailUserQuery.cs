using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Users
{
    public record GetByEmailUserQuery(string Email) : IRequest<UserViewDTO>;
}
