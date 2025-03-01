using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public class LockContactRequestCommandHandler(IContactService contactService) : IRequestHandler<LockContactRequestCommand, string>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<string> Handle(LockContactRequestCommand request, CancellationToken cancellationToken)
        {
            return await _contactService.LockContactRequestAsyncService(request.IdSender, request.UserToLock);
        }
    }
}
