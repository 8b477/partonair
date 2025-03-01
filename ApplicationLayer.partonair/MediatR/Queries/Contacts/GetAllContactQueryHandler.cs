using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetAllContactQueryHandler(IContactService contactService) : IRequestHandler<GetAllContactQuery, ICollection<ContactViewDTO>>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ICollection<ContactViewDTO>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetAllAsyncService();
        }
    }
}
