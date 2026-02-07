FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Shared/MoneyBee.Shared/MoneyBee.Shared.csproj", "Shared/MoneyBee.Shared/"]
COPY ["src/Services/MoneyBee.Transfer/MoneyBee.Transfer.Domain/MoneyBee.Transfer.Domain.csproj", "Services/MoneyBee.Transfer/MoneyBee.Transfer.Domain/"]
COPY ["src/Services/MoneyBee.Transfer/MoneyBee.Transfer.Application/MoneyBee.Transfer.Application.csproj", "Services/MoneyBee.Transfer/MoneyBee.Transfer.Application/"]
COPY ["src/Services/MoneyBee.Transfer/MoneyBee.Transfer.Infrastructure/MoneyBee.Transfer.Infrastructure.csproj", "Services/MoneyBee.Transfer/MoneyBee.Transfer.Infrastructure/"]
COPY ["src/Services/MoneyBee.Transfer/MoneyBee.Transfer.API/MoneyBee.Transfer.API.csproj", "Services/MoneyBee.Transfer/MoneyBee.Transfer.API/"]

RUN dotnet restore "Services/MoneyBee.Transfer/MoneyBee.Transfer.API/MoneyBee.Transfer.API.csproj"

COPY . .
WORKDIR "/src/src/Services/MoneyBee.Transfer/MoneyBee.Transfer.API"
RUN dotnet publish "MoneyBee.Transfer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MoneyBee.Transfer.API.dll"]