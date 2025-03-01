using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public record GetByNameContactQuery(string Name) : IRequest<ICollection<ContactViewDTO>>;
}
