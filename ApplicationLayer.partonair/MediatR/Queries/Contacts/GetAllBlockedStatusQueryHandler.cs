using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetAllBlockedStatusQueryHandler(IContactService contactService) : IRequestHandler<GetAllBlockedStatusQuery, ICollection<ContactViewDTO>>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ICollection<ContactViewDTO>> Handle(GetAllBlockedStatusQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetAllBlockedStatusAsyncService(request.Status);
        }
    }
}
