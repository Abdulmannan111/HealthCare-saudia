# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-stage
WORKDIR /app

# Copy the solution and project files
COPY *.sln ./
COPY ./Al-Munazam/API.csproj ./Al-Munazam/
COPY ./Application/Application.csproj ./Application/
COPY ./Domain/Domain.csproj ./Domain/
COPY ./Infrastructure/Infrastructure.csproj ./Infrastructure/

# Restore dependencies
RUN dotnet restore "./Al-Munazam/API.csproj"

# Copy the entire project directories
COPY ./Al-Munazam/. ./Al-Munazam/
COPY ./Application/. ./Application/
COPY ./Domain/. ./Domain/
COPY ./Infrastructure/. ./Infrastructure/

# Build the application
RUN dotnet publish "./Al-Munazam/API.csproj" -c Release -o /app/out

# Stage 2: Serve the application from the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-stage
ENV ASPNETCORE_URLS=http://+:5001
Expose 5001
WORKDIR /app
COPY --from=build-stage /app/out .

# Set the entry point
ENTRYPOINT ["dotnet", "API.dll"]
