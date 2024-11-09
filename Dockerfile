# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Podme.API/Podme.API.csproj", "Podme.API/"]
COPY ["Podme.Application/Podme.Application.csproj", "Podme.Application/"]
COPY ["Podme.Domain/Podme.Domain.csproj", "Podme.Domain/"]
COPY ["Podme.Infrastructure/Podme.Infrastructure.csproj", "Podme.Infrastructure/"]
RUN dotnet restore "Podme.API/Podme.API.csproj"

# Copy all source code
COPY . .

# Build the application
WORKDIR "/src/Podme.API"
RUN dotnet build "Podme.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "Podme.API.csproj" -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Podme.API.dll"]