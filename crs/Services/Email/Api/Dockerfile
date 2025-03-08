# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["crs/Services/Email/Api/Api.csproj", "crs/Services/Email/Api/"]
COPY ["crs/Common/Api.Core/Api.Core.csproj", "crs/Common/Api.Core/"]
COPY ["crs/Common/Contracts/Contracts.csproj", "crs/Common/Contracts/"]
COPY ["crs/Common/Domain.Core/Domain.Core.csproj", "crs/Common/Domain.Core/"]
COPY ["crs/Common/Infrasctrurcture.Core/Infrasctrurcture.Core.csproj", "crs/Common/Infrasctrurcture.Core/"]
COPY ["crs/Services/Email/Application/Application.csproj", "crs/Services/Email/Application/"]
COPY ["crs/Common/Application.Core/Application.Core.csproj", "crs/Common/Application.Core/"]
COPY ["crs/Services/Email/Infrascturture/Infrasctructure.csproj", "crs/Services/Email/Infrascturture/"]
RUN dotnet restore "./crs/Services/Email/Api/Api.csproj"
COPY . .
WORKDIR "/src/crs/Services/Email/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]