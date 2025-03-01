using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Evaluations
{
    public record GetByGuidEvaluationQuery(Guid Id) : IRequest<EvaluationViewDTO>;

}
