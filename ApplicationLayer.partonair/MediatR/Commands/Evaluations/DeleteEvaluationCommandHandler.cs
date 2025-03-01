using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public class DeleteEvaluationCommandHandler(IEvaluationService evaluationService) : IRequestHandler<DeleteEvaluationCommand>
    {
        private readonly IEvaluationService _evaluationService = evaluationService;
        public async Task Handle(DeleteEvaluationCommand request, CancellationToken cancellationToken)
        {
            await _evaluationService.DeleteAsyncService(request.Id);
        }
    }
}
