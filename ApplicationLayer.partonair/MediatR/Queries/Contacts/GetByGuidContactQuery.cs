using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public record GetByGuidContactQuery(Guid Id) : IRequest<ContactViewDTO>;
}
