#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ISTUDIO.Web.Api/ISTUDIO.Web.Api.csproj", "ISTUDIO.Web.Api/"]
COPY ["ISTUDIO.Infrastructure/ISTUDIO.Infrastructure.csproj", "ISTUDIO.Infrastructure/"]
COPY ["ISTUDIO.Application/ISTUDIO.Application.csproj", "ISTUDIO.Application/"]
COPY ["ISTUDIO.Domain/ISTUDIO.Domain.csproj", "ISTUDIO.Domain/"]
RUN dotnet restore "./ISTUDIO.Web.Api/ISTUDIO.Web.Api.csproj"
COPY . .
WORKDIR "/src/ISTUDIO.Web.Api"
RUN dotnet build "./ISTUDIO.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ISTUDIO.Web.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ISTUDIO.Web.Api.dll"]