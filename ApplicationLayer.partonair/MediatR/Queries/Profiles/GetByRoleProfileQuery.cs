using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Profiles
{
    public record GetByRoleProfileQuery(string Role) : IRequest<ICollection<ProfileAndUserViewDTO>>;
}
