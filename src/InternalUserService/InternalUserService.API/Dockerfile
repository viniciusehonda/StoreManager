# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["Directory.Build.props", "."]
COPY ["src/InternalUserService.Api/InternalUserService.Api.csproj", "src/InternalUserService.Api/"]
COPY ["src/InternalUserService.Infrastructure/InternalUserService.Infrastructure.csproj", "src/InternalUserService.Infrastructure/"]
COPY ["src/InternalUserService.Application/InternalUserService.Application.csproj", "src/InternalUserService.Application/"]
COPY ["src/InternalUserService.Domain/InternalUserService.Domain.csproj", "src/InternalUserService.Domain/"]
COPY ["src/SharedKernel/SharedKernel.csproj", "src/SharedKernel/"]
RUN dotnet restore "./src/InternalUserService.Api/InternalUserService.Api.csproj"
COPY . .
WORKDIR "/src/src/InternalUserService.Api"
RUN dotnet build "./InternalUserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InternalUserService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InternalUserService.Api.dll"]