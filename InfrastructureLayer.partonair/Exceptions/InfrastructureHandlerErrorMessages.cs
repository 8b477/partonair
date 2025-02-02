using Infrastructure.partonair.Enums;


namespace InfrastructureLayer.partonair.Exceptions
{
    /// <summary>
    /// Manages and centralizes error messages from the Infrastructure layer.
    /// </summary>
    public static class InfrastructureHandlerErrorMessages
    {
        private static readonly Dictionary<InfrastructureErrorType, string> Messages = new()
    {
        { InfrastructureErrorType.ResourceNotFound, "The requested resource was not found." },
        // .. Other messages here ..
    };

        /// <summary>
        /// Provides a pertinent message based on the error type thrown in the Infrastructure layer.
        /// </summary>
        /// <param name="errorType">Enum representing the type of error in the Infrastructure layer.</param>
        /// <returns>Returns the appropriate message for the given error type.</returns>
        public static string GetMessage(InfrastructureErrorType errorType)
        {
            return Messages.TryGetValue(errorType, out var message)
                ? message
                : "An unknown error occurred.";
        }
    }

}
