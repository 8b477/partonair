using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public record UpdateEvaluationCommand(EvaluationUpdateDTO Eval) : IRequest<EvaluationViewDTO>;
}
