FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["API.partonair/API.partonair.csproj", "API.partonair/"]
COPY ["ApplicationLayer.partonair/ApplicationLayer.partonair.csproj", "ApplicationLayer.partonair/"]
COPY ["DomainLayer.partonair/DomainLayer.partonair.csproj", "DomainLayer.partonair/"]
COPY ["InfrastructureLayer.partonair/InfrastructureLayer.partonair.csproj", "InfrastructureLayer.partonair/"]
RUN dotnet restore "API.partonair/API.partonair.csproj"
COPY . .
WORKDIR "/src/API.partonair"
RUN dotnet build "API.partonair.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.partonair.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.partonair.dll"]
