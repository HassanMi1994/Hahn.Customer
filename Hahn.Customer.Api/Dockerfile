#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Hahn.Customer.Api/Hahn.Customers.Api.csproj", "Hahn.Customer.Api/"]
COPY ["Hahn.Customer.Domain/Hahn.Customers.Domain.csproj", "Hahn.Customer.Domain/"]
COPY ["Hahn.Customer.Infrastructure/Hahn.Customers.Infrastructure.csproj", "Hahn.Customer.Infrastructure/"]
RUN dotnet restore "Hahn.Customer.Api/Hahn.Customers.Api.csproj"
COPY . .
WORKDIR "/src/Hahn.Customer.Api"
RUN dotnet build "Hahn.Customers.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hahn.Customers.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hahn.Customers.Api.dll"]