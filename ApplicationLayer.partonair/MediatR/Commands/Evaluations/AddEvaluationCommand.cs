using ApplicationLayer.partonair.DTOs;

using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public record AddEvaluationCommand(Guid IdOwner, EvaluationCreateDTO Eval) : IRequest<EvaluationViewDTO>;
}
