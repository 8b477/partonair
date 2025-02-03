namespace InfrastructureLayer.partonair.Enums
{
    public enum InfrastructureErrorType
    {
        ResourceNotFound,
        DatabaseConnectionError,
        CreateDatabaseException,
        ConcurrencyDatabaseException,
        UpdateDatabaseException,
        CancelationDatabaseException,
        NoActiveTransactionException,
        UnexpectedDatabaseException,
    }
}
