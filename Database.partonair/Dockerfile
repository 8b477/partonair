# Dockerfile pour le service db
FROM mcr.microsoft.com/mssql/server:2019-latest

# Passer en mode root pour installer les paquets nécessaires
USER root

# Installer curl, gnupg, unixodbc-dev et mssql-tools
RUN apt-get update && \
    apt-get install -y curl gnupg unixodbc-dev && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools && \
    rm -rf /var/lib/apt/lists/*

# Revenir à l'utilisateur mssql pour exécuter SQL Server
USER mssql

# Commande par défaut : démarrer SQL Server
ENTRYPOINT [ "/opt/mssql/bin/sqlservr" ]
