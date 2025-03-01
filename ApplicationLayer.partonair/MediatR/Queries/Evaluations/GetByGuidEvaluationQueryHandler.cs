using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Evaluations
{
    public class GetByGuidEvaluationQueryHandler(IEvaluationService evaluationService) : IRequestHandler<GetByGuidEvaluationQuery, EvaluationViewDTO>
    {
        private readonly IEvaluationService _evaluationService = evaluationService;
        public async Task<EvaluationViewDTO> Handle(GetByGuidEvaluationQuery request, CancellationToken cancellationToken)
        {
            return await _evaluationService.GetByGuidAsyncService(request.Id);
        }
    }
}
