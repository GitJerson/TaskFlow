# TaskFlow

A task management REST API built with ASP.NET Core Web API (.NET 9). TaskFlow lets teams organize work through projects, tasks, and assignments. Users can create projects, break them into tasks, assign teammates, set deadlines, and track progress through a straightforward REST API.

This is an ongoing build. Some features are already working, others are still being added.

## Current Status

### Done
- Project structure and folder architecture (Controllers, Services, Repositories, Models, DTOs, Middleware, Data)
- PostgreSQL connection via dotnet user-secrets
- Entity Framework Core with AppDbContext
- User model and initial database migration
- Auth endpoints: POST /api/v1/auth/register and POST /api/v1/auth/login
- Service and Repository pattern wired up with proper DI
- Password hashing with BCrypt
- Real registration and login logic hitting the database
- JWT token generation on login
- OAuth 2.0 with Google login
- API Key authentication with BCrypt hashing, one-time key reveal, and revocation support
- Projects CRUD with soft delete and JWT-protected routes
- Tasks CRUD with priority, assignment, soft delete, and nested routing
- Comments CRUD with soft delete, nested routing, and user attribution
- Global error handling middleware with clean JSON error responses
- Request logging with Serilog to console and file output
- Rate limiting with fixed window, 100 requests per minute per IP, returns 429
- API versioning with URL segment versioning, all endpoints under /api/v1/
- Redis caching on project endpoints with cache invalidation on create, update, and delete

### In Progress
- Background jobs with Hangfire for due date email reminders
- Health check endpoint at /health
- Docker and Docker Compose setup
- CI/CD with GitHub Actions

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 9) |
| Language | C# |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Caching | Redis (StackExchange.Redis) |
| Background Jobs | Hangfire (planned) |
| Logging | Serilog |
| Validation | FluentValidation (planned) |
| Object Mapping | AutoMapper (planned) |
| Password Hashing | BCrypt.Net |
| JWT Authentication | Microsoft.AspNetCore.Authentication.JwtBearer |
| OAuth 2.0 | Microsoft.AspNetCore.Authentication.Google |
| API Keys | Custom attribute-based authentication |
| Rate Limiting | ASP.NET Core built-in Rate Limiter |
| API Versioning | Asp.Versioning.Mvc |
| Containerization | Docker (planned) |
| CI/CD | GitHub Actions (planned) |

## Project Structure

```
TaskFlow.API/
├── Controllers/
│   └── V1/             # Version 1 controllers
├── Services/           # Business logic
├── Repositories/       # Data access layer
├── Models/             # Database entities
├── DTOs/               # Request and response objects
├── Middleware/         # Custom middleware pipeline
├── Data/               # AppDbContext
├── Helpers/            # Utility classes
└── Configs/            # Configuration models
```

### Architecture

```
Controller -> Service -> Repository -> DbContext
```

Each layer only talks to the layer directly below it. Controllers never touch the database directly.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) for running Redis
- [Git](https://git-scm.com/)

### Fork and Clone

**1. Fork the repository**

Hit the Fork button at the top right of this page.

**2. Clone your fork**
```bash
git clone https://github.com/GitJerson/TaskFlow.git
cd TaskFlow/api
```

**3. Add the original repo as upstream**
```bash
git remote add upstream https://github.com/GitJerson/TaskFlow.git
git remote -v
```

### Configuration

This project uses dotnet user-secrets so no secrets ever go in appsettings.json or get committed to Git.

**1. Initialize user secrets**
```bash
dotnet user-secrets init
```

**2. Set your database connection string**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=yourdb;Username=yourusername;Password=yourpassword"
```

**3. Set your JWT secret**
```bash
dotnet user-secrets set "Jwt:Secret" "your-secret-key"
```

**4. Set your Redis connection string**
```bash
dotnet user-secrets set "ConnectionStrings:Redis" "localhost:6379"
```

**5. Set your Google OAuth credentials**
```bash
dotnet user-secrets set "Google:ClientId" "your-google-client-id"
dotnet user-secrets set "Google:ClientSecret" "your-google-client-secret"
```

### Start Redis

```bash
docker run -d -p 6379:6379 --name redis redis
```

### Install Dependencies

```bash
dotnet restore
```

### Run Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Run the API

```bash
dotnet run
```

For development with auto-restart on file changes:
```bash
dotnet watch run
```

## API Endpoints

### Auth
| Method | Endpoint | Status |
|---|---|---|
| POST | `/api/v1/auth/register` | Working |
| POST | `/api/v1/auth/login` | Working |
| GET | `/api/v1/auth/google` | Working |
| GET | `/api/v1/auth/google/callback` | Working |

### Projects
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/project` | Working |
| GET | `/api/v1/project/{id}` | Working |
| POST | `/api/v1/project` | Working |
| PUT | `/api/v1/project/{id}` | Working |
| DELETE | `/api/v1/project/{id}` | Working |

### Tasks
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/project/{projectId}/tasks` | Working |
| GET | `/api/v1/tasks/{taskId}` | Working |
| POST | `/api/v1/project/{projectId}/tasks` | Working |
| PUT | `/api/v1/tasks/{taskId}` | Working |
| DELETE | `/api/v1/tasks/{taskId}` | Working |

### Comments
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/tasks/{id}/comments` | Working |
| POST | `/api/v1/tasks/{id}/comments` | Working |
| GET | `/api/v1/comment/{id}` | Working |
| PUT | `/api/v1/comment/{id}` | Working |
| DELETE | `/api/v1/comment/{id}` | Working |

### API Keys
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/apikeys` | Working |
| POST | `/api/v1/apikeys` | Working |
| PUT | `/api/v1/apikeys/{keyId}` | Working |
| DELETE | `/api/v1/apikeys/{keyId}` | Working |

### System
| Method | Endpoint | Status |
|---|---|---|
| GET | `/health` | In Progress |

## Contributing

1. Fork the repo
2. Create a feature branch
```bash
git checkout -b feature/your-feature-name
```
3. Commit your changes
```bash
git commit -m "feat: add your feature"
```
4. Push and open a Pull Request

## Related

**TaskFlow Client** is the frontend for this API. Not yet available, currently in planning. Coming soon at [github.com/GitJerson/TaskFlow-Client](https://github.com/GitJerson/TaskFlow-Client)

## License

MIT License