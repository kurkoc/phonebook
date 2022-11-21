FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/apigateway/Phonebook.ApiGateway/Phonebook.ApiGateway.csproj", "src/apigateway/Phonebook.ApiGateway/"]
RUN dotnet restore "src/apigateway/Phonebook.ApiGateway/Phonebook.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/apigateway/Phonebook.ApiGateway"
RUN dotnet build "Phonebook.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phonebook.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phonebook.ApiGateway.dll"]