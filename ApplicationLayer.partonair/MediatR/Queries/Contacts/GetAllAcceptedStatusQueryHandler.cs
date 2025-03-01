using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetAllAcceptedStatusQueryHandler(IContactService contactService) : IRequestHandler<GetAllAcceptedStatusQuery, ICollection<ContactViewDTO>>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ICollection<ContactViewDTO>> Handle(GetAllAcceptedStatusQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetAllAcceptedStatusAsyncService(request.Status);
        }
    }
}
