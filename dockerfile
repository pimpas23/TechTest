# Use the .NET SDK image with version 6.0
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore

# Set the working directory in the container
WORKDIR /app
EXPOSE 8080

# Copy the project files to the container
COPY ["Makefile","Makefile"]
Copy ["Jenkinsfile","Jenkinsfile"]
COPY ["TechTest.sln", "TechTest.sln"]
COPY ["src/TechTest.Api/TechTest.Api.csproj", "src/TechTest.Api/"]
COPY ["src/TechTest.Business/TechTest.Business.csproj", "src/TechTest.Business/"]
COPY ["src/TechTest.Data/TechTest.Data.csproj", "src/TechTest.Data/"]

COPY ["tests/UnitTests/TechTest.Api.Tests/TechTest.Api.Tests.csproj", "tests/UnitTests/TechTest.Api.Tests/"]
COPY ["tests/UnitTests/TechTest.Business.Tests/TechTest.Business.Tests.csproj", "tests/UnitTests/TechTest.Business.Tests/"]
COPY ["tests/UnitTests/TechTest.Data.Tests/TechTest.Data.Tests.csproj", "tests/UnitTests/TechTest.Data.Tests/"]

COPY ["tests/IntegrationTests/TechTest.Api.IntegrationTests/TechTest.Api.IntegrationTests.csproj", "tests/IntegrationTests/TechTest.Api.IntegrationTests/"]

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
WORKDIR /app


# Unit tests
From build AS unit-tests
ENTRYPOINT dotnet test tests/UnitTests/TechTest.Api.Tests/TechTest.Api.Tests.csproj --no-build -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx" && \
		   dotnet test tests/UnitTests/TechTest.Business.Tests/TechTest.Business.Tests.csproj --no-build -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx" && \
		   dotnet test tests/UnitTests/TechTest.Data.Tests/TechTest.Data.Tests.csproj --no-build -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx"

# IntegrationTests
From build as integration-tests
ENTRYPOINT dotnet test tests/IntegrationTests/TechTest.Api.IntegrationTests/TechTest.Api.IntegrationTests.csproj --no-build -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx"

# Define the entry point for the container
From build AS final
WORKDIR /app/publish
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "TechTest.Api.dll"]