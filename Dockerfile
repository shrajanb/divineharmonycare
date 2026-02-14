FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["DivineHarmonyCare.csproj", "."]
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Create data directories for JSON storage
RUN mkdir -p /app/wwwroot/data/intakes /app/wwwroot/data/invoices

ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "DivineHarmonyCare.dll"]
