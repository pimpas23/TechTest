# Use the .NET SDK image with version 6.0
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore

# Set the working directory in the container
WORKDIR /app
EXPOSE 8080

# Copy the project files to the container
COPY ["TechTest.sln", "TechTest.sln"]
COPY ["src/TechTest.Api/TechTest.Api.csproj", "src/TechTest.Api/"]
COPY ["src/TechTest.Business/TechTest.Business.csproj", "src/TechTest.Business/"]
COPY ["src/TechTest.Data/TechTest.Data.csproj", "src/TechTest.Data/"]

# Restore NuGet packages and clear cache
RUN dotnet restore TechTest.sln
RUN dotnet nuget locals all --clear

# Copy the source code
COPY ["src", "src/"]
COPY ["tests", "tests/"]

# Build
FROM restore AS build
RUN dotnet build TechTest.sln -c Release
RUN dotnet publish "src/TechTest.Api/TechTest.Api.csproj" -c Release --no-build --output /app/publish
RUN mkdir /app/publish/logs

# Set the working directory in the container
WORKDIR /app/publish

# Define the entry point for the container
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "TechTest.Api.dll"]