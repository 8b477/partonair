
using ApplicationLayer.partonair.DTOs;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Queries.Profiles
{
    public record class GetByIdProfileQuery(Guid IdUser) : IRequest<ProfileViewDTO>;
}
