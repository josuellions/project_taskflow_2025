# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY taskflow.sln ./
COPY src/taskflow.API/ ./src/taskflow.API/
COPY tests/UseCases.Test/ ./tests/UseCases.Test/

RUN dotnet restore ./src/taskflow.API/taskflow.API.csproj
RUN dotnet publish ./src/taskflow.API/taskflow.API.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "taskflow.API.dll"]
