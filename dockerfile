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
COPY ["tests/TechTest.Api.Tests/TechTest.Api.Tests.csproj", "tests/TechTest.Api.Tests/"]
COPY ["tests/TechTest.Business.Tests/TechTest.Business.Tests.csproj", "tests/TechTest.Business.Tests/"]
COPY ["tests/TechTest.Data.Tests/TechTest.Data.Tests.csproj", "tests/TechTest.Data.Tests/"]

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
ENTRYPOINT dotnet test tests/TechTest.Api.Tests/TechTest.Api.Tests.csproj -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx" && \
		   dotnet test tests/TechTest.Business.Tests/TechTest.Business.Tests.csproj --no-build -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx" && \
		   dotnet test tests/TechTest.Data.Tests/TechTest.Data.Tests.csproj --no-build -c Release --results-directory /reports --logger "console;verbosity=detailed" --logger "trx"



# Define the entry point for the container
From build AS final
WORKDIR /app/publish
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "TechTest.Api.dll"]