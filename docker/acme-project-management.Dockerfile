FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build

WORKDIR /src
COPY . .

RUN ["dotnet", "publish", "src/ProjectManagement.Api/ProjectManagement.Api.csproj", "-c", "Release", "-o", "/app"]

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

EXPOSE 5000
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://*:5000

ENTRYPOINT ["dotnet", "/app/ProjectManagement.Api.dll"]