FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PhoneShop.Ordering.WebApi/PhoneShop.Ordering.WebApi.csproj", "PhoneShop.Ordering.WebApi/"]
COPY ["src/PhoneShop.Ordering.Application/PhoneShop.Ordering.Application.csproj", "PhoneShop.Ordering.Application/"]
COPY ["src/PhoneShop.Ordering.Domain/PhoneShop.Ordering.Domain.csproj", "PhoneShop.Ordering.Domain/"]
COPY ["src/PhoneShop.Ordering.Infrastructure/PhoneShop.Ordering.Infrastructure.csproj", "PhoneShop.Ordering.Infrastructure/"]

COPY . .
WORKDIR /src
RUN dotnet build "src/PhoneShop.Ordering.WebApi/PhoneShop.Ordering.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/PhoneShop.Ordering.WebApi/PhoneShop.Ordering.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneShop.Ordering.WebApi.dll"]