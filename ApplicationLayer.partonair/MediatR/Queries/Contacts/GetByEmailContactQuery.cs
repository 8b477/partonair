using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public record GetByEmailContactQuery(string Email) : IRequest<ContactViewDTO>;
}
