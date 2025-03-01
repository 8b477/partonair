using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public class AddContactCommandHandler(IContactService contactService) : IRequestHandler<AddContactCommand, ContactViewDTO>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<ContactViewDTO> Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            return await _contactService.CreateAsyncService(request.Contact);
        }
    }
}
