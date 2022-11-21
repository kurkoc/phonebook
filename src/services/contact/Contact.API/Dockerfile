FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/contact/Contact.API/Contact.API.csproj", "src/services/contact/Contact.API/"]
COPY ["src/services/contact/Contact.Application/Contact.Application.csproj", "src/services/contact/Contact.Application/"]
COPY ["src/building-blocks/BuildingBlock.Application/BuildingBlock.Application.csproj", "src/building-blocks/BuildingBlock.Application/"]
COPY ["src/building-blocks/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "src/building-blocks/BuildingBlocks.Domain/"]
COPY ["src/services/contact/Contact.Domain/Contact.Domain.csproj", "src/services/contact/Contact.Domain/"]
COPY ["src/services/contact/Contact.Infrastructure/Contact.Infrastructure.csproj", "src/services/contact/Contact.Infrastructure/"]
COPY ["src/building-blocks/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/building-blocks/BuildingBlocks.Infrastructure/"]
RUN dotnet restore "src/services/contact/Contact.API/Contact.API.csproj"
COPY . .
WORKDIR "/src/src/services/contact/Contact.API"
RUN dotnet build "Contact.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.API.dll"]