using DomainLayer.partonair.Exceptions.Enums;
using DomainLayer.partonair.Exceptions.Handler;


namespace DomainLayer.partonair.Exceptions
{
    public class ApplicationLayerException : Exception
    { 
        public ApplicationLayerErrorType ErrorType { get; private set; }

        public ApplicationLayerException(ApplicationLayerErrorType errorType, string additionalInfo = "")
        : base(FormatExceptionMessage(errorType, additionalInfo))
        {
            ErrorType = errorType;
        }
        private static string FormatExceptionMessage(ApplicationLayerErrorType errorType, string additionalInfo)
        {
            string baseMessage = ErrorMessageHandler.GetMessage(errorType);
            return string.IsNullOrEmpty(additionalInfo) ? baseMessage : $"{baseMessage} {additionalInfo}".Trim();
        }
    }
}
