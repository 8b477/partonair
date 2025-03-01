using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public record UpdateEvaluationCommand(Guid IdEval, EvaluationUpdateDTO Eval) : IRequest<EvaluationViewDTO>;
}
