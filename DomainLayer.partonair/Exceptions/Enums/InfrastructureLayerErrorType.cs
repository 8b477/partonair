﻿namespace DomainLayer.partonair.Exceptions.Enums
{
    public enum InfrastructureLayerErrorType
    {
        ResourceNotFoundException,
        EntityIsNullException,
        DatabaseConnectionErrorException,
        CreateDatabaseException,
        ConcurrencyDatabaseException,
        UpdateDatabaseException,
        CancelationDatabaseException,
        NoActiveTransactionException,
        UnexpectedDatabaseException,
    }
}
