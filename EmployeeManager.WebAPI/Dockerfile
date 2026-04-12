# Multi-stage Dockerfile for building and running the EmployeeManager.WebAPI project
# Stage 1: Use the .NET SDK image to build the application. The SDK image contains
# the tools (dotnet build/publish) required to compile the project.
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Set the working directory inside the container for the build stage. All following
# RUN/COPY commands will be relative to this path.
WORKDIR /src

# Copy only the WebAPI project file first so that `dotnet restore` can use Docker layer
# caching and avoid re-downloading packages when source files change.
COPY ["EmployeeManager.WebAPI/EmployeeManager.WebAPI.csproj", "EmployeeManager.WebAPI/"]
# Copy referenced project files so `dotnet restore` can resolve project-to-project references.
COPY ["EmployeeManager.Domain/EmployeeManager.Domain.csproj", "EmployeeManager.Domain/"]
COPY ["EmployeeManager.Entity/EmployeeManager.Entity.csproj", "EmployeeManager.Entity/"]
COPY ["EmployeeManager.Base/EmployeeManager.Base.csproj", "EmployeeManager.Base/"]
COPY ["EmployeeManager.Common/EmployeeManager.Common.csproj", "EmployeeManager.Common/"]

# Restore NuGet packages for the WebAPI project. This creates a cached layer with
# restored packages which speeds up subsequent builds if package references haven't changed.
RUN dotnet restore "EmployeeManager.WebAPI/EmployeeManager.WebAPI.csproj"

# Copy the entire repository into the container. This happens after restore to keep
# the restore layer cache effective when only source files change.
COPY . .

# Change working directory to the WebAPI project folder so build/publish commands
# run in the correct project context.
WORKDIR /src/EmployeeManager.WebAPI

# Build the project in Release configuration. Output goes to /app/build inside the build stage
# (intermediate artifact - not the final published output).
RUN dotnet build "EmployeeManager.WebAPI.csproj" -c Release -o /app/build

# Stage 2: publish the app. `dotnet publish` produces the compiled app and all
# runtime dependencies into a single folder ready to be copied to the runtime image.
FROM build AS publish
RUN dotnet publish "EmployeeManager.WebAPI.csproj" -c Release -o /app/publish

# Stage 3: Create the runtime image using the smaller ASP.NET runtime image which
# contains only what is required to run an ASP.NET Core application.
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

# Set working directory in the final runtime image where the app will be placed.
WORKDIR /app

# Expose port 80 (HTTP) and 443 (HTTPS). This documents which ports the container
# will listen on; Dockerfile EXPOSE is informational but some orchestration tools use it.
EXPOSE 80
EXPOSE 443

# Configure ASP.NET Core to listen on port 80 inside the container.
ENV ASPNETCORE_URLS="http://+:80"

# Copy the published output from the publish stage into the runtime image at /app.
COPY --from=publish /app/publish .

# Default command to run the application. This starts Kestrel and the Web API.
ENTRYPOINT ["dotnet", "EmployeeManager.WebAPI.dll"]
