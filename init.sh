#!/bin/bash

echo "Waiting for SQL Server to be ready..."
until /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -Q "SELECT 1" > /dev/null 2>&1; do
  sleep 1
done

echo "SQL Server is ready. Running initialization scripts..."

# Exécute les scripts d'initialisation
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /scripts/init.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -d master -i /scripts/Starter.PostDeployment.sql

echo "Scripts executed successfully!"
