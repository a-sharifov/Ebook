#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["crs/Services/Order/Api/Api.csproj", "crs/Services/Order/Api/"]
COPY ["crs/Common/Api.Core/Api.Core.csproj", "crs/Common/Api.Core/"]
COPY ["crs/Common/Contracts/Contracts.csproj", "crs/Common/Contracts/"]
COPY ["crs/Common/Domain.Core/Domain.Core.csproj", "crs/Common/Domain.Core/"]
COPY ["crs/Services/Order/Application/Application.csproj", "crs/Services/Order/Application/"]
COPY ["crs/Services/Order/Domain/Domain.csproj", "crs/Services/Order/Domain/"]
COPY ["crs/Services/Order/Infrastructure/Infrastructure.csproj", "crs/Services/Order/Infrastructure/"]
COPY ["crs/Services/Order/Persistence/Persistence.csproj", "crs/Services/Order/Persistence/"]
COPY ["crs/Services/Order/Presentation/Presentation.csproj", "crs/Services/Order/Presentation/"]
RUN dotnet restore "./crs/Services/Order/Api/./Api.csproj"
COPY . .
WORKDIR "/src/crs/Services/Order/Api"
RUN dotnet build "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
