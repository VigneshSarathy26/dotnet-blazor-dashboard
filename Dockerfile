# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all csproj files and restore
COPY ["DotNetBlazorDashboard.slnx", "."]
COPY ["Directory.Build.props", "."]
COPY ["stylecop.json", "."]
COPY [".editorconfig", "."]
COPY ["DotNetBlazorDashboard.Web/DotNetBlazorDashboard.Web.csproj", "DotNetBlazorDashboard.Web/"]
COPY ["DotNetBlazorDashboard.Web.Client/DotNetBlazorDashboard.Web.Client.csproj", "DotNetBlazorDashboard.Web.Client/"]
COPY ["DotNetBlazorDashboard.Application/DotNetBlazorDashboard.Application.csproj", "DotNetBlazorDashboard.Application/"]
COPY ["DotNetBlazorDashboard.Domain/DotNetBlazorDashboard.Domain.csproj", "DotNetBlazorDashboard.Domain/"]
COPY ["DotNetBlazorDashboard.Infrastructure/DotNetBlazorDashboard.Infrastructure.csproj", "DotNetBlazorDashboard.Infrastructure/"]
COPY ["DotNetBlazorDashboard.Tests/DotNetBlazorDashboard.Tests.csproj", "DotNetBlazorDashboard.Tests/"]

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR "/src/DotNetBlazorDashboard.Web"
RUN dotnet build "DotNetBlazorDashboard.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DotNetBlazorDashboard.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetBlazorDashboard.Web.dll"]
