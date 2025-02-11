#!/bin/bash

# Attendre que SQL Server soit prêt
echo "Waiting for SQL Server to be ready..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
while [ $? -ne 0 ]; do
    echo "SQL Server is not ready yet. Waiting..."
    sleep 10
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "SELECT 1" > /dev/null 2>&1
done
echo "SQL Server is ready. Running schema and seed scripts..."

# Créer la base de données si elle n'existe pas
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -Q "IF DB_ID('partonair') IS NULL CREATE DATABASE partonair"

# Attendre que la base 'partonair' soit créée
echo "Waiting for database 'partonair' to be available..."
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d partonair -Q "SELECT 1" > /dev/null 2>&1
while [ $? -ne 0 ]; do
    echo "Database 'partonair' is not ready yet. Waiting..."
    sleep 10
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d partonair -Q "SELECT 1" > /dev/null 2>&1
done
echo "Database 'partonair' is ready."

# Exécuter les scripts de création et d'insertion
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d partonair -i /scripts/schema.sql
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "$SA_PASSWORD" -d partonair -i /scripts/seed-data.sql

echo "Database initialization completed successfully!"
