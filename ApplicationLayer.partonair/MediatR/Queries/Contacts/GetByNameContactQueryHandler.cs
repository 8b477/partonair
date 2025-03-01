using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Contacts
{
    public class GetByNameContactQueryHandler(IContactService contactService) : IRequestHandler<GetByNameContactQuery, ICollection<ContactViewDTO>>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ICollection<ContactViewDTO>> Handle(GetByNameContactQuery request, CancellationToken cancellationToken)
        {
            return await _contactService.GetByNameAsyncService(request.Name);
        }
    }
}
