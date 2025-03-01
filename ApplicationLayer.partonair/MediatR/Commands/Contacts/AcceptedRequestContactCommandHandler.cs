using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    internal class AcceptedRequestContactCommandHandler(IContactService contactService) : IRequestHandler<AcceptedRequestContactCommand, string>
    {
        protected readonly IContactService _contactService = contactService;
        public async Task<string> Handle(AcceptedRequestContactCommand request, CancellationToken cancellationToken)
        {
            return await _contactService.AcceptedRequestAsyncService(request.IdContact);
        }
    }
}
