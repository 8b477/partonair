using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public record AddEvaluationCommand(EvaluationCreateDTO Eval) : IRequest<EvaluationViewDTO>;
}
