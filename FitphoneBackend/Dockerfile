# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore (caches layer)
COPY ["FitphoneBackend.csproj", "./"]
RUN dotnet restore "FitphoneBackend.csproj"

# Copy everything else
COPY . .

# Build and publish (let it restore if needed)
WORKDIR /src
RUN dotnet publish "FitphoneBackend.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FitphoneBackend.dll"]