using ApplicationLayer.partonair.Exceptions.Enums;


namespace ApplicationLayer.partonair.Exceptions
{
    /// <summary>
    /// Manages and centralizes error messages from the Application layer.
    /// </summary>
    public static class ApplicationLayerHandlerErrorMessages
    {
        private static readonly Dictionary<ApplicationLayerErrorType, string> Messages = new()
        {
            { ApplicationLayerErrorType.ConstraintViolationError, "The request conflicted with a constraint." },
        };

        /// <summary>
        /// Provides a pertinent message based on the error type thrown in the Application layer.
        /// </summary>
        /// <param name="errorType">Enum representing the type of error in the Application layer.</param>
        /// <returns>Returns the appropriate message for the given error type.</returns>
        public static string GetMessage(ApplicationLayerErrorType errorType)
        {
            return Messages.TryGetValue(errorType, out var message)
                ? message
                : "An unknown error occurred.";
        }
    }
}
