FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/report/Report.API/Report.API.csproj", "src/services/report/Report.API/"]
COPY ["src/building-blocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "src/building-blocks/BuildingBlock.Application/"]
COPY ["src/building-blocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "src/building-blocks/BuildingBlocks.Domain/"]
COPY ["src/building-blocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/building-blocks/BuildingBlocks.Infrastructure/"]
RUN dotnet restore "src/services/report/Report.API/Report.API.csproj"
COPY . .
WORKDIR "/src/src/services/report/Report.API"
RUN dotnet build "Report.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Report.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Report.API.dll"]