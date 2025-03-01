using MediatR;


namespace ApplicationLayer.partonair.MediatR.Commands.Evaluations
{
    public record DeleteEvaluationCommand(Guid Id) : IRequest;
}
