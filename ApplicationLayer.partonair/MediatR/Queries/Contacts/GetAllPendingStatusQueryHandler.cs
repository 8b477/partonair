using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetAllPendingStatusQueryHandler(IContactService contactService) : IRequestHandler<GetAllPendingStatusQuery, ICollection<ContactViewDTO>>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ICollection<ContactViewDTO>> Handle(GetAllPendingStatusQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetAllPendingStatusAsyncService(request.Status);
        }
    }
}
