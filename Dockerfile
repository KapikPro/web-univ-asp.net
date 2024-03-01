# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY lab1/WebApplication2/*.csproj ./WebApplication2/
RUN dotnet restore

# copy everything else and build app
COPY lab1/WebApplication2/. ./WebApplication2/
WORKDIR /source/WebApplication2
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebApplication2.dll"]
