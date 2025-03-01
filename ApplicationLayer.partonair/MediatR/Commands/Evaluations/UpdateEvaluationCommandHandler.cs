using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public class UpdateEvaluationCommandHandler(IEvaluationService evaluationService) : IRequestHandler<UpdateEvaluationCommand, EvaluationViewDTO>
    {
        private readonly IEvaluationService _evaluationService = evaluationService;
        public async Task<EvaluationViewDTO> Handle(UpdateEvaluationCommand request, CancellationToken cancellationToken)
        {
            return await _evaluationService.UpdateAsyncService(request.IdEval,request.Eval);
        }
    }
}
