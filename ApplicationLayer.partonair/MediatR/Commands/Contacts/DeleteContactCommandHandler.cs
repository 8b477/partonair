using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Contacts
{
    public class DeleteContactCommandHandler(IContactService contactService) : IRequestHandler<DeleteContactCommand>
    {
        private readonly IContactService _contactService = contactService;
        public Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            return _contactService.DeleteAsyncService(request.IdSender,request.IdContact);
        }
    }
}
