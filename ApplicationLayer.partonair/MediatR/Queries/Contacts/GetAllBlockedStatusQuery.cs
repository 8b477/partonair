using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public record GetAllBlockedStatusQuery(string Status) : IRequest<ICollection<ContactViewDTO>>;
}
