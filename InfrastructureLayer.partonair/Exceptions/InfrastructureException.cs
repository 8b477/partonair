using Infrastructure.partonair.Enums;


namespace InfrastructureLayer.partonair.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureErrorType ErrorType { get; private set; }

        public InfrastructureException(InfrastructureErrorType errorType, string additionalInfo)
        : base($"{InfrastructureHandlerErrorMessages.GetMessage(errorType)} {additionalInfo}")
        {
            ErrorType = errorType;
        }

    }
}
