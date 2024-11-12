FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebConfigurationControl.Api/WebConfigurationControl.WebApi.csproj", "WebConfigurationControl.Api/"]
COPY ["WebConfigurationControl.Application/WebConfigurationControl.Application.csproj", "WebConfigurationControl.Application/"]
COPY ["WebConfigurationControl.Common/WebConfigurationControl.Common.csproj", "WebConfigurationControl.Common/"]
COPY ["WebConfigurationControl.Infrastructure/WebConfigurationControl.Infrastructure.csproj", "WebConfigurationControl.Infrastructure/"]
COPY ["WebConfigurationControl.Domain/WebConfigurationControl.Domain.csproj", "WebConfigurationControl.Domain/"]
COPY ["WebConfigurationControl.NotificationModels/WebConfigurationControl.NotificationModels.csproj", "WebConfigurationControl.NotificationModels/"]
COPY ["WebConfigurationControl.Notifications/WebConfigurationControl.Notifications.csproj", "WebConfigurationControl.Notifications/"]
RUN dotnet restore "./WebConfigurationControl.Api/WebConfigurationControl.WebApi.csproj"
COPY . .
WORKDIR "/src/WebConfigurationControl.Api"
RUN dotnet build "./WebConfigurationControl.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebConfigurationControl.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebConfigurationControl.WebApi.dll"]