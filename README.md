# 🚀 DotNet Blazor Dashboard

A modern, enterprise-ready web dashboard built on **ASP.NET Core** and **Blazor Interactive Auto Mode**. Designed for high performance, cross-platform flexibility, and scalable multi-cloud deployment.

---

## 🏗 Architecture
The application follows **Clean Architecture** and **Domain-Driven Design (DDD)** principles, ensuring a clear separation of concerns and a testable codebase.

- **Presentation Layer**: Blazor Web App (Interactive Auto) providing a seamless SSR + WASM experience.
- **Application Layer**: CQRS pattern using MediatR for handling commands and queries.
- **Domain Layer**: Pure domain models, entities, and business rules.
- **Infrastructure Layer**: Data persistence (EF Core), external service integrations, and cross-cutting concerns.

---

## 🛠 Tech Stack
| Component | Technology |
| :--- | :--- |
| **Framework** | .NET 10.0 (ASP.NET Core) |
| **UI Library** | Blazor (Server + WebAssembly) |
| **Databases** | PostgreSQL & MS-SQL Server |
| **Caching** | Redis (Distributed Cache) |
| **Reverse Proxy** | Nginx |
| **Observability** | Prometheus & Grafana |
| **Containerization** | Docker & Docker Compose |

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
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) (for local development)

### Option A: Docker Compose (Full Stack)
The fastest way to spin up the entire environment:
```bash
docker-compose up -d --build
```

### Port Mappings & Interfaces
| Service | Internal Port | External Port | URL |
| :--- | :--- | :--- | :--- |
| **Main Web Dashboard** | 8080 (via Nginx) | 80 | [http://localhost](http://localhost) |
| **ASP.NET API** | 8080 | 5000 | [http://localhost:5000](http://localhost:5000) |
| **PostgreSQL** | 5432 | 5432 | `localhost:5432` |
| **MS-SQL Server** | 1433 | 1433 | `localhost:1433` |
| **Redis** | 6379 | 6379 | `localhost:6379` |
| **Prometheus** | 9090 | 9090 | [http://localhost:9090](http://localhost:9090) |

---

### Option B: Local Native Development (dotnet SDK)
If you prefer to run the .NET app natively on your host machine but keep the infrastructure dependencies in docker:

1. **Spin up dependencies only**:
   ```bash
   docker-compose up -d pgdb redis prometheus
   ```
2. **Environment Configuration**:
   Copy `.env.example` to `.env` and adjust connection strings.
   ```bash
   cp .env.example .env
   ```
3. **Run the application**:
   ```bash
   cd DotNetBlazorDashboard.Web
   dotnet run
   ```

---

## 📥 API Reference
### Ingestion Endpoints
`POST /api/ingestion/record`
Ingest a single billing record.

`POST /api/ingestion/batch`
Ingest a batch of billing records for bulk processing.

---

## 🧪 Testing
Includes a comprehensive test suite using xUnit and Moq:
- **Unit Tests**: Domain logic and Application services.
- **Integration Tests**: Repository-to-DB validation.
- **E2E Tests**: API testing using `WebApplicationFactory`.

Run all tests:
```bash
dotnet test
```

---

## 📊 Observability & Monitoring
- **Prometheus**: Metrics collection via `/metrics` endpoint.
- **Nginx Logs**: Accessible via `docker logs nginx`.
- **Structured Logging**: Integrated with Microsoft.Extensions.Logging.

---

## 📂 Project Structure
```text
├── DotNetBlazorDashboard.Web/           # ASP.NET Core host (Blazor Web App)
├── DotNetBlazorDashboard.Web.Client/    # Blazor WASM components (Client part)
├── DotNetBlazorDashboard.Application/   # Use-cases (CQRS, MediatR)
├── DotNetBlazorDashboard.Domain/        # Entities, Value Objects, Enums
├── DotNetBlazorDashboard.Infrastructure/ # EF Core, Redis, Normalizers
├── DotNetBlazorDashboard.Tests/         # Unit, Integration, E2E tests
├── infra/                               # Prometheus, Nginx config
├── Dockerfile                           # Multi-stage production build
└── docker-compose.yml                   # Local dev stack
```

---

## 🔧 Troubleshooting

> [!WARNING]
> **.NET 10 Preview Packages**: If the `Infrastructure` project fails to build, check if the SQL Server or Npgsql preview packages are correctly resolved in your NuGet cache.

- **Check Container Status**: `docker-compose ps`
- **View Logs**: `docker-compose logs -f dotNetBlazorDashboard.web`
- **Database Connection Issues**: Ensure the `DatabaseProvider` environment variable matches your target DB (PostgreSQL/SqlServer).
