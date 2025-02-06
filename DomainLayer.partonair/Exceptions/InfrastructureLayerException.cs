using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions.Handler;


namespace DomainLayer.partonair.Exceptions
{
    public class InfrastructureLayerException : Exception
    {
        public InfrastructureLayerErrorType ErrorType {  get; private set; }
        public InfrastructureLayerException(InfrastructureLayerErrorType errorType, string additionalInfo = "")
            : base(FormatExceptionMessage(errorType, additionalInfo))
        {
            ErrorType = errorType;
        }
        private static string FormatExceptionMessage(InfrastructureLayerErrorType errorType, string additionalInfo)
        {
            string baseMessage = ErrorMessageHandler.GetMessage(errorType);
            return string.IsNullOrEmpty(additionalInfo) ? baseMessage : $"{baseMessage} {additionalInfo}".Trim();
        }
    }
}
