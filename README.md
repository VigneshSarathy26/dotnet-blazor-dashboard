# 🚀 DotNet Blazor Dashboard

A modern, enterprise-ready web dashboard built on **.NET 10.0**, **ASP.NET Core**, and **Blazor Interactive Auto Mode** (Server SSR + Client WebAssembly). Designed for high performance, cross-platform flexibility, and scalable multi-cloud deployment.

---

## 🏗 Architecture
The application follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles, ensuring a clear separation of concerns, testability, and maintainability.

```
       ┌────────────────────────────────────────────────────────┐
       │              Presentation Layer (Blazor)               │
       │   DotNetBlazorDashboard.Web (ASP.NET Core Host / SSR)   │
       │   DotNetBlazorDashboard.Web.Client (WASM Components)   │
       └───────────────────────────┬────────────────────────────┘
                                   │
                                   ▼
       ┌────────────────────────────────────────────────────────┐
       │               Application Layer (CQRS)                 │
       │     DotNetBlazorDashboard.Application (MediatR)        │
       └───────────────────────────┬────────────────────────────┘
                                   │
                                   ▼
       ┌────────────────────────────────────────────────────────┐
       │                  Domain Layer (DDD)                    │
       │       DotNetBlazorDashboard.Domain (Entities/VOs)      │
       └───────────────────────────┬────────────────────────────┘
                                   │
                                   ▼
       ┌────────────────────────────────────────────────────────┐
       │                  Infrastructure Layer                  │
       │    DotNetBlazorDashboard.Infrastructure (EF Core,      │
       │     PostgreSQL, MS-SQL Server, Redis Normalizers)      │
       └────────────────────────────────────────────────────────┘
```

- **Presentation Layer**: Blazor Web App (Interactive Auto) providing a seamless pre-rendered Server-Side Rendering (SSR) bootstrap transitioning to WebAssembly (WASM).
- **Application Layer**: CQRS pattern using MediatR for commands, queries, and handlers.
- **Domain Layer**: Pure business logic, aggregates, entities, and value objects.
- **Infrastructure Layer**: Data persistence (EF Core), database providers (PostgreSQL / SQL Server), distributed caching (Redis), and cloud service normalizers.

---

## 🛠 Tech Stack

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Framework** | .NET 10.0 (ASP.NET Core) | Latest LTS enterprise framework |
| **UI Library** | Blazor (Interactive Auto) | Dual-mode Server + Client WebAssembly |
| **Databases** | PostgreSQL & MS-SQL Server | Entity Framework Core 10.0 with dual provider support |
| **Distributed Cache** | Redis | Session state & high-performance query caching |
| **Reverse Proxy** | Nginx | Reverse proxy, static asset delivery, and SSL termination |
| **Unit Testing** | xUnit & NUnit | Multi-framework support for unit and integration tests |
| **E2E Testing** | Microsoft Playwright | Automated browser-level end-to-end UI testing |
| **Code Quality & Linting** | StyleCop.Analyzers | Compile-time static analysis and style enforcement |
| **IaC (Infrastructure)** | Terraform, Bicep, ARM | Modular cloud provisioning for Azure environments |
| **Containerization** | Docker & Docker Compose | Multi-stage production builds & local orchestration |

---

## ☁️ Supported Cloud Providers
Built with multi-cloud flexibility in mind, featuring specialized service normalizers for:
- **AWS**: S3, RDS, CloudWatch integrations.
- **Azure**: Blob Storage, SQL Database, App Insights.
- **GCP**: Cloud Storage, Cloud SQL, Stackdriver.

---

## 🚀 Quick Start

### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (for local CLI development)
- PowerShell 7+ (recommended for cross-platform scripts)

---

### Option A: Docker Compose (Full Stack)

Spin up the entire stack including Nginx, Web application, PostgreSQL, MS-SQL Server, and Redis:

```bash
docker compose up -d --build
```

#### Port Mappings & Interfaces

| Service | Internal Port | External Port | URL |
| :--- | :--- | :--- | :--- |
| **Main Web Dashboard (Nginx)** | 8080 | 80 | [http://localhost](http://localhost) |
| **Direct ASP.NET Web App** | 8080 | 5000 | [http://localhost:5000](http://localhost:5000) |
| **PostgreSQL** | 5432 | 5432 | `localhost:5432` |
| **MS-SQL Server** | 1433 | 1433 | `localhost:1433` |
| **Redis** | 6379 | 6379 | `localhost:6379` |

To stop the containers:
```bash
docker compose down
```

---

### Option B: Local Development (Host .NET SDK)

If you prefer to run the .NET application locally on your host while keeping dependencies in Docker:

1. **Spin up backend dependencies only**:
   ```bash
   docker compose up -d pgdb redis
   ```

2. **Environment Configuration**:
   Copy `.env.example` to `.env` and set your desired database provider (`PostgreSQL` or `SqlServer`):
   ```bash
   cp .env.example .env
   ```

3. **Run the application**:
   ```bash
   cd DotNetBlazorDashboard.Web
   dotnet run
   ```

---

## 🧪 Testing & Code Quality

The repository includes a comprehensive testing suite and compile-time code analysis.

### 1. Unit & Integration Tests (xUnit & NUnit)
The test project supports both **xUnit** and **NUnit** test runners:

```bash
# Run all tests in the solution
dotnet test

# Run only xUnit tests
dotnet test --filter "FullyQualifiedName~UnitTest1"

# Run only NUnit tests
dotnet test --filter "FullyQualifiedName~SampleNUnitTests"
```

### 2. End-to-End (E2E) UI Testing (Playwright)
Browser-level automated UI tests use **Microsoft Playwright**:

```bash
# 1. Install required browser binaries (one-time setup)
pwsh DotNetBlazorDashboard.Tests/bin/Debug/net10.0/playwright.ps1 install

# 2. Run Playwright E2E tests
dotnet test --filter "FullyQualifiedName~Playwright"
```

### 3. Linting & Static Code Analysis (StyleCop & dotnet format)
Style rules and formatting conventions are enforced via **StyleCop.Analyzers** and **.editorconfig**:
- `Directory.Build.props`: Globally enables analyzer rules across all projects.
- `stylecop.json`: Configures StyleCop settings, company metadata, and using directives.
- `.editorconfig`: Configures whitespace, indentation, naming conventions, and diagnostic severity.

#### A. Run Linting Checks (Verification Mode - CI & Pre-build)
Check if the codebase conforms to all style, formatting, and analyzer rules without modifying files (exits with non-zero code on violation):
```bash
# Verify formatting, style conventions, and analyzer rules across the solution
dotnet format --verify-no-changes --severity warn

# Or run specific check categories independently:
dotnet format whitespace --verify-no-changes
dotnet format style --verify-no-changes
dotnet format analyzers --verify-no-changes
```

#### B. Auto-Fix Linting & Formatting Issues
Automatically format and fix rule violations across all projects in the solution:
```bash
# Automatically fix formatting, style, and analyzer issues
dotnet format
```

#### C. Run StyleCop Analysis via Build
Run static code analysis during compilation and treat warnings as errors (recommended for CI pipelines):
```bash
# Clean build enforcing zero analyzer warnings
dotnet build --no-incremental /warnaserror
```

---

## 📂 Project Structure

```text
├── .editorconfig                                 # Code formatting & StyleCop rules
├── Directory.Build.props                         # Global MSBuild properties & analyzers
├── stylecop.json                                 # StyleCop configuration
├── Dockerfile                                    # Multi-stage production build
├── docker-compose.yml                            # Local dev environment orchestration
├── DotNetBlazorDashboard.slnx                    # Solution file
├── nginx/
│   └── nginx.conf                                # Nginx reverse proxy configuration
├── infra/                                        # Infrastructure as Code (IaC)
│   ├── terraform/                                # Terraform root modules & environments
│   │   ├── environments/ (dev, prod)
│   │   └── modules/azure/ (compute, database, networking)
│   ├── azure_bicep/                              # Azure Bicep modules & environments
│   └── azure_arm_template/                       # ARM JSON deployment templates
├── DotNetBlazorDashboard.Web/                    # ASP.NET Core Host (Blazor Server SSR)
│   ├── Components/                               # App root, pages, and layouts
│   ├── Program.cs                                # Application startup and DI
│   └── appsettings.json                          # Configuration & connection strings
├── DotNetBlazorDashboard.Web.Client/             # Blazor WebAssembly Client project
│   ├── Pages/                                    # Interactive WASM components
│   └── _Imports.razor                            # Client namespaces & directives
├── DotNetBlazorDashboard.Application/            # CQRS commands, queries, MediatR handlers
├── DotNetBlazorDashboard.Domain/                 # Entities, Value Objects, Domain logic
├── DotNetBlazorDashboard.Infrastructure/         # EF Core, PostgreSQL/MSSQL, Redis caching
│   ├── Data/DashboardDbContext.cs                # EF Core DbContext
│   └── Extensions/ServiceCollectionExtensions.cs # Infrastructure DI extensions
└── DotNetBlazorDashboard.Tests/                  # Test suite
    ├── UnitTest1.cs                              # xUnit sample tests
    ├── SampleNUnitTests.cs                       # NUnit sample tests
    └── PlaywrightE2ETests.cs                     # Playwright E2E browser tests
```

---

## 🔧 Troubleshooting

### 1. Docker Compose Image Tag Error (`repository name must be lowercase`)
Docker requires image tags to be lowercase. In `docker-compose.yml`, services must use lowercase names (e.g. `web` instead of `dotNetBlazorDashboard.web`).

### 2. Blazor WebAssembly ReadyToRun Compilation (`NETSDK1095`)
ReadyToRun (`PublishReadyToRun=true`) cross-compiles machine code for native CPU targets (`linux-x64`), which is incompatible with `browser-wasm` client projects. The client project explicitly sets `<PublishReadyToRun>false</PublishReadyToRun>`.

### 3. EF Core Package Version Conflicts (`NU1605: Detected package downgrade`)
Ensure all `Microsoft.EntityFrameworkCore.*` packages in `DotNetBlazorDashboard.Infrastructure.csproj` align to the same version (e.g., `10.0.5`).

### 4. Viewing Container Logs
```bash
# View web application logs
docker compose logs -f web

# View reverse proxy logs
docker compose logs -f nginx

# Check status of all containers
docker compose ps
```
