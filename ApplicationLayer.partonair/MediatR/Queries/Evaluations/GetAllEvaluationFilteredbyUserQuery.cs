using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Queries.Evaluations
{
    public record GetAllEvaluationFilteredbyUserQuery() : IRequest<ICollection<EvaluationViewDTO>>;
}
