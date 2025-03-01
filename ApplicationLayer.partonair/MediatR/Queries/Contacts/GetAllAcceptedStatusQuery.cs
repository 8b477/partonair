using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public record GetAllAcceptedStatusQuery(string Status) : IRequest<ICollection<ContactViewDTO>>;
}
