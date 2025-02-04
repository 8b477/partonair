using ApplicationLayer.partonair.Exceptions.Enums;


namespace ApplicationLayer.partonair.Exceptions
{
    public class ApplicationLayerException(ApplicationLayerErrorType errorType, string additionalInfo = "")
        : Exception($"{ApplicationLayerHandlerErrorMessages.GetMessage(errorType)} {additionalInfo}")
    {
        public ApplicationLayerErrorType ErrorType { get; private set; } = errorType;
    }
}
