namespace DomainLayer.partonair.Exceptions.Enums
{
    public enum InfrastructureLayerErrorType
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
