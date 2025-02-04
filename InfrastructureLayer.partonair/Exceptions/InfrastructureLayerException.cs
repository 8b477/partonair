using InfrastructureLayer.partonair.Exceptions.Enums;


namespace InfrastructureLayer.partonair.Exceptions
{
    public class InfrastructureLayerException(InfrastructureLayerErrorType errorType, string additionalInfo = "")
        : Exception($"{InfrastructureLayerHandlerErrorMessages.GetMessage(errorType)} {additionalInfo}")
    {
        public InfrastructureLayerErrorType ErrorType { get; private set; } = errorType;
    }
}
