# TaskFlow API

A task management REST API built with ASP.NET Core Web API (.NET 9). TaskFlow lets teams organize work through projects, tasks, and assignments. Moreover, users can create projects, break them into tasks, assign teammates, set deadlines, and track progress through a straightforward REST API.

This is an ongoing build. Some features are already working, others are still being added.

---

## Current Status

### ✅ Done
- Project structure and folder architecture (Controllers, Services, Repositories, Models, DTOs, Middleware, Data)
- PostgreSQL connection via dotnet user-secrets
- Entity Framework Core with AppDbContext
- User model and initial database migration
- Auth endpoints — `POST /api/auth/register` and `POST /api/auth/login`
- Service and Repository pattern wired up with proper DI

### 🚧 In Progress
- Password hashing with BCrypt
- Real registration and login logic hitting the database
- JWT token generation on login
- OAuth 2.0 — Google and GitHub login
- API Key authentication
- Projects CRUD
- Tasks CRUD with filtering and pagination
- Comments on tasks
- Global error handling middleware
- Request logging with Serilog
- Rate limiting
- API versioning (`/api/v1/...`)
- Redis caching
- Background jobs with Hangfire — due date email reminders
- Health check endpoint (`/health`)
- Docker & Docker Compose setup
- CI/CD with GitHub Actions

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 9) |
| Language | C# |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Caching | Redis *(planned)* |
| Background Jobs | Hangfire *(planned)* |
| Logging | Serilog *(planned)* |
| Validation | FluentValidation |
| Object Mapping | AutoMapper |
| Password Hashing | BCrypt.Net *(in progress)* |
| Containerization | Docker *(planned)* |
| CI/CD | GitHub Actions *(planned)* |

---

## Project Structure

```
TaskFlow.API/
├── Controllers/        # HTTP request/response handlers
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
Controller → Service → Repository → DbContext
```
Each layer only talks to the layer directly below it e.g controllers will never touch the database directly.

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download/) *(required once caching is implemented)*
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) *(optional for now)*
- [Git](https://git-scm.com/)

---

### Fork & Clone

**1. Fork the repository**

Hit the **Fork** button at the top right of this page.

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

---

### Configuration

This project uses **dotnet user-secrets** — no secrets ever go in `appsettings.json` or get committed to Git.

**1. Initialize user secrets**
```bash
dotnet user-secrets init
```

**2. Set your database connection string**
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Database=yourdb;Username=yourusername;Password=yourpassword"
```

More secrets will be added here as features like JWT and OAuth get implemented.

---

### Install Dependencies

```bash
dotnet restore
```

---

### Run Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### Run the API

```bash
dotnet run
```

---

## API Endpoints

### Auth ✅
| Method | Endpoint | Status |
|---|---|---|
| POST | `/api/auth/register` | ✅ Working |
| POST | `/api/auth/login` | ✅ Working |
| POST | `/api/auth/google` | 🚧 In Progress |

### Projects 🚧
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/projects` | 🚧 In Progress |
| GET | `/api/v1/projects/{id}` | 🚧 In Progress |
| POST | `/api/v1/projects` | 🚧 In Progress |
| PUT | `/api/v1/projects/{id}` | 🚧 In Progress |
| DELETE | `/api/v1/projects/{id}` | 🚧 In Progress |

### Tasks 🚧
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/projects/{id}/tasks` | 🚧 In Progress |
| GET | `/api/v1/tasks/{id}` | 🚧 In Progress |
| POST | `/api/v1/projects/{id}/tasks` | 🚧 In Progress |
| PUT | `/api/v1/tasks/{id}` | 🚧 In Progress |
| DELETE | `/api/v1/tasks/{id}` | 🚧 In Progress |

### Comments 🚧
| Method | Endpoint | Status |
|---|---|---|
| GET | `/api/v1/tasks/{id}/comments` | 🚧 In Progress |
| POST | `/api/v1/tasks/{id}/comments` | 🚧 In Progress |
| DELETE | `/api/v1/comments/{id}` | 🚧 In Progress |

### System 🚧
| Method | Endpoint | Status |
|---|---|---|
| GET | `/health` | 🚧 In Progress |

---

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

---

## License

MIT License