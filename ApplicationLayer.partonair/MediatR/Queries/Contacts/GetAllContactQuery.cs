using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public record GetAllContactQuery : IRequest<ICollection<ContactViewDTO>>;
}
