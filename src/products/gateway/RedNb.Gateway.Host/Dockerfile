#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/gateways/web/RedNb.Gateway.Host/RedNb.Gateway.Host.csproj", "src/gateways/web/RedNb.Gateway.Host/"]
RUN dotnet restore "src/gateways/web/RedNb.Gateway.Host/RedNb.Gateway.Host.csproj"
COPY . .
WORKDIR "/src/src/gateways/web/RedNb.Gateway.Host"
RUN dotnet build "RedNb.Gateway.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RedNb.Gateway.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RedNb.Gateway.Host.dll"]