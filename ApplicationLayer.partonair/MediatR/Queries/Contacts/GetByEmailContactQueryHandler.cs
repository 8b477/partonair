using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetByEmailContactQueryHandler(IContactService contactService) : IRequestHandler<GetByEmailContactQuery, ContactViewDTO>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ContactViewDTO> Handle(GetByEmailContactQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetByEmailAsyncService(request.Email);
        }
    }
}
