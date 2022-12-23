#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/services/bases/auth/RedNb.Auth.Host/RedNb.Auth.Host.csproj", "src/services/bases/auth/RedNb.Auth.Host/"]
COPY ["src/services/bases/auth/RedNb.Auth.Application/RedNb.Auth.Application.csproj", "src/services/bases/auth/RedNb.Auth.Application/"]
COPY ["src/services/bases/auth/RedNb.Auth.Application.Contracts/RedNb.Auth.Application.Contracts.csproj", "src/services/bases/auth/RedNb.Auth.Application.Contracts/"]
COPY ["src/core/RedNb.Core/RedNb.Core.csproj", "src/core/RedNb.Core/"]
COPY ["src/services/bases/auth/RedNb.Auth.Domain.Shared/RedNb.Auth.Domain.Shared.csproj", "src/services/bases/auth/RedNb.Auth.Domain.Shared/"]
COPY ["src/services/bases/auth/RedNb.Auth.Domain/RedNb.Auth.Domain.csproj", "src/services/bases/auth/RedNb.Auth.Domain/"]
COPY ["src/services/bases/auth/RedNb.Auth.EntityFrameworkCore/RedNb.Auth.EntityFrameworkCore.csproj", "src/services/bases/auth/RedNb.Auth.EntityFrameworkCore/"]
RUN dotnet restore "src/services/bases/auth/RedNb.Auth.Host/RedNb.Auth.Host.csproj"
COPY . .
WORKDIR "/src/src/services/bases/auth/RedNb.Auth.Host"
RUN dotnet build "RedNb.Auth.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedNb.Auth.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedNb.Auth.Host.dll"]