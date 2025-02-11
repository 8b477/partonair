# Dockerfile pour le service db
FROM mcr.microsoft.com/mssql/server:2019-latest

# Passer en mode root pour installer les paquets nécessaires
USER root

# Installer les outils SQL Server et les dépendances nécessaires
RUN apt-get update && \
    apt-get install -y curl gnupg unixodbc-dev && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools && \
    rm -rf /var/lib/apt/lists/*

# Ajouter les outils SQL Server au PATH pour faciliter l'utilisation
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Copier le script d'initialisation dans l'image
COPY ./init.sh /init.sh
RUN chmod +x /init.sh

# Revenir à l'utilisateur mssql pour exécuter SQL Server
USER mssql

# Commande par défaut : démarrer SQL Server et exécuter le script d'initialisation
ENTRYPOINT ["/bin/bash", "-c", "/init.sh & /opt/mssql/bin/sqlservr"]
