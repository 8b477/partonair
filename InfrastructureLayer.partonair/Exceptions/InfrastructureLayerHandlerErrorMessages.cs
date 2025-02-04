using InfrastructureLayer.partonair.Exceptions.Enums;


namespace InfrastructureLayer.partonair.Exceptions
{
    /// <summary>
    /// Manages and centralizes error messages from the Infrastructure layer.
    /// </summary>
    public static class InfrastructureLayerHandlerErrorMessages
    {
        private static readonly Dictionary<InfrastructureLayerErrorType, string> Messages = new()
    {
        { InfrastructureLayerErrorType.ResourceNotFound, "The requested resource was not found." },
        { InfrastructureLayerErrorType.DatabaseConnectionError, "Database connection failed" },
        { InfrastructureLayerErrorType.ConcurrencyDatabaseException, "A concurrency conflict occurred while saving changes" },
        { InfrastructureLayerErrorType.UpdateDatabaseException, "A database update error occurred while saving changes" },
        { InfrastructureLayerErrorType.CancelationDatabaseException, "The operation was canceled" },
        { InfrastructureLayerErrorType.UnexpectedDatabaseException, "An unexpected error occurred while saving changes" },
        { InfrastructureLayerErrorType.NoActiveTransactionException, "Attempted to commit a transaction when no transaction was active." },

    };

        /// <summary>
        /// Provides a pertinent message based on the error type thrown in the Infrastructure layer.
        /// </summary>
        /// <param name="errorType">Enum representing the type of error in the Infrastructure layer.</param>
        /// <returns>Returns the appropriate message for the given error type.</returns>
        public static string GetMessage(InfrastructureLayerErrorType errorType)
        {
            return Messages.TryGetValue(errorType, out var message)
                ? message
                : "An unknown error occurred.";
        }
    }

}
