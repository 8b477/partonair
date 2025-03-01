using ApplicationLayer.partonair.DTOs;
using ApplicationLayer.partonair.Interfaces;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Evaluations
{
    public class GetAllEvaluationFilteredbyUserQueryHandler(IEvaluationService evaluationService) : IRequestHandler<GetAllEvaluationFilteredbyUserQuery, ICollection<EvaluationViewDTO>>
    {
        private readonly IEvaluationService _evaluationService = evaluationService;
        public async Task<ICollection<EvaluationViewDTO>> Handle(GetAllEvaluationFilteredbyUserQuery request, CancellationToken cancellationToken)
        {
            return await _evaluationService.GetAllGrouppByUserAsyncService();
        }
    }
}
