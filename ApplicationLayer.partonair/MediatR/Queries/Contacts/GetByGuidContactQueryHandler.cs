using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetByGuidContactQueryHandler(IContactService contactService) : IRequestHandler<GetByGuidContactQuery, ContactViewDTO>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ContactViewDTO> Handle(GetByGuidContactQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetByGuidAsyncService(request.Id);
        }
    }
}
