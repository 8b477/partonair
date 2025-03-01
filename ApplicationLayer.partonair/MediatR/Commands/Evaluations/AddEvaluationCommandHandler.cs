using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;

namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public class AddEvaluationCommandHandler(IEvaluationService evaluationService) : IRequestHandler<AddEvaluationCommand, EvaluationViewDTO>
    {
        private readonly IEvaluationService _evaluationService = evaluationService;
        public async Task<EvaluationViewDTO> Handle(AddEvaluationCommand request, CancellationToken cancellationToken)
        {
            return await _evaluationService.CreateAsyncService(request.Eval);
        }
    }
}
