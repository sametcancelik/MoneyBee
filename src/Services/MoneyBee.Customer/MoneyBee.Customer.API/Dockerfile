FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/Shared/MoneyBee.Shared/MoneyBee.Shared.csproj", "Shared/MoneyBee.Shared/"]
COPY ["src/Services/MoneyBee.Customer/MoneyBee.Customer.Domain/MoneyBee.Customer.Domain.csproj", "Services/MoneyBee.Customer/MoneyBee.Customer.Domain/"]
COPY ["src/Services/MoneyBee.Customer/MoneyBee.Customer.Application/MoneyBee.Customer.Application.csproj", "Services/MoneyBee.Customer/MoneyBee.Customer.Application/"]
COPY ["src/Services/MoneyBee.Customer/MoneyBee.Customer.Infrastructure/MoneyBee.Customer.Infrastructure.csproj", "Services/MoneyBee.Customer/MoneyBee.Customer.Infrastructure/"]
COPY ["src/Services/MoneyBee.Customer/MoneyBee.Customer.API/MoneyBee.Customer.API.csproj", "Services/MoneyBee.Customer/MoneyBee.Customer.API/"]

RUN dotnet restore "Services/MoneyBee.Customer/MoneyBee.Customer.API/MoneyBee.Customer.API.csproj"

COPY . .
WORKDIR "/src/src/Services/MoneyBee.Customer/MoneyBee.Customer.API"
RUN dotnet publish "MoneyBee.Customer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MoneyBee.Customer.API.dll"]