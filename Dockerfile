# Use the official .NET SDK image as a base
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy the solution
COPY . ./

# Restore dependencies
RUN dotnet restore

# Publish the specific project
RUN dotnet publish ./DocumentsWebApi/DocumentsWebApi.csproj -c Release -o out

# Create a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENTRYPOINT ["dotnet", "DocumentsWebApi.dll"]