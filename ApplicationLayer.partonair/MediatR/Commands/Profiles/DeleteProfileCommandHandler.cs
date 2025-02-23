using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Profiles
{
    public class DeleteProfileCommandHandler(IProfileService profilService) : IRequestHandler<DeleteProfileCommand>
    {
        private readonly IProfileService _profileService = profilService;
        public Task Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            return  _profileService.DeleteAsyncService(request.Id);
        }
    }
}
