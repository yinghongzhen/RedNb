#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/auth/RedNb.Auth.HttpApi.Host/RedNb.Auth.HttpApi.Host.csproj", "src/services/auth/RedNb.Auth.HttpApi.Host/"]
RUN dotnet restore "src/services/auth/RedNb.Auth.HttpApi.Host/RedNb.Auth.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/services/auth/RedNb.Auth.HttpApi.Host"
RUN dotnet build "RedNb.Auth.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedNb.Auth.HttpApi.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedNb.Auth.HttpApi.Host.dll"]