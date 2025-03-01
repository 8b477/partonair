using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public class UnlockContactRequestCommandHandler(IContactService contactService) : IRequestHandler<UnlockContactRequestCommand, string>
    {
        private readonly IContactService _contactService = contactService;
        public async Task<string> Handle(UnlockContactRequestCommand request, CancellationToken cancellationToken)
        {
            return await _contactService.UnlockContactRequestAsyncService(request.IdSender, request.UserToUnLock);
        }
    }
}
