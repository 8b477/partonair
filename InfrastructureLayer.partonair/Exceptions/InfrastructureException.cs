using InfrastructureLayer.partonair.Enums;


namespace InfrastructureLayer.partonair.Exceptions
{
    public class InfrastructureException(InfrastructureErrorType errorType, string additionalInfo = "")
        : Exception($"{InfrastructureHandlerErrorMessages.GetMessage(errorType)} {additionalInfo}")
    {
        public InfrastructureErrorType ErrorType { get; private set; } = errorType;
    }
}
