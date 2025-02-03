using InfrastructureLayer.partonair.Enums;


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
        { InfrastructureErrorType.DatabaseConnectionError, "Database connection failed" },
        { InfrastructureErrorType.ConcurrencyDatabaseException, "A concurrency conflict occurred while saving changes" },
        { InfrastructureErrorType.UpdateDatabaseException, "A database update error occurred while saving changes" },
        { InfrastructureErrorType.CancelationDatabaseException, "The operation was canceled" },
        { InfrastructureErrorType.UnexpectedDatabaseException, "An unexpected error occurred while saving changes" },
        { InfrastructureErrorType.NoActiveTransactionException, "Attempted to commit a transaction when no transaction was active." },

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
