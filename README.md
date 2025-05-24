# TaskManager API

Welcome to **TaskManager**, your lightweight, “kitchen-style” to-do list REST API. Orders (tasks) come in via HTTP, get cooked (persisted) in SQL Server, and served back with full CRUD support—complete with interactive docs, tests, and a clean, layered design.

---

## Table of Contents

1. [✨ Features](#-features)  
2. [🛠️ Tech Stack](#️-tech-stack)  
3. [🏗️ Architecture Overview](#️-architecture-overview)  
4. [🚀 Getting Started](#-getting-started)  
   - [Prerequisites](#prerequisites)  
   - [Installation & Configuration](#installation--configuration)  
   - [Database Migrations](#database-migrations)  
5. [▶️ Running the App](#️-running-the-app)  
   - [Local: `dotnet run`](#local-dotnet-run)  
   - [Interactive Docs: Swagger UI](#interactive-docs-swagger-ui)  
   - [REST Client & cURL](#rest-client--curl)  
6. [🧪 Testing](#-testing)  
   - [Unit Tests](#unit-tests)  
   - [Integration Tests](#integration-tests)  
7. [🐋 Containerization (Optional)](#-containerization-optional)  
8. [⚙️ CI/CD & Next Steps](#️-cicd--next-steps)  
9. [🤝 Contributing](#-contributing)  
10. [📄 License](#-license)  

---

## Features

- **Full CRUD** on `TaskItem` via `/api/tasks`  
- **SQL Server** persistence (LocalDB or Docker) with EF Core  
- **Auto-migrations** on startup—no manual scripts  
- **Swagger/OpenAPI** UI for interactive exploration  
- **Layered architecture**: Controllers → Services → Repositories → DbContext  
- **Robust testing**:  
  - **Unit tests** (xUnit + Moq)  
  - **Integration tests** (TestServer / WebApplicationFactory)  

---

## Tech Stack

| Layer             | Technology                                    |
|-------------------|-----------------------------------------------|
| API               | ASP.NET Core Web API                          |
| Data Access       | Entity Framework Core (Code-First)            |
| Database          | SQL Server (LocalDB or Docker container)      |
| Dependency Inj.   | Built-in ASP.NET Core DI                      |
| Documentation     | Swashbuckle (Swagger)                         |
| Testing           | xUnit, Moq, Microsoft.AspNetCore.Mvc.Testing  |

---

## Architecture Overview

Client → Controllers → Services → Repositories → DbContext → SQL Server
↑
Tests (Unit & Integration)


- **Controllers**: HTTP endpoints, routing  
- **Services**: Business logic, validation  
- **Repositories**: Data access via EF Core  
- **DbContext**: ORM context managing `TaskItem` entities  
- **Tests**:  
  - Unit tests mock repositories  
  - Integration tests spin up an in-memory server  

---

## 🚀 Getting Started

### Prerequisites

- [.NET 6 SDK (or higher)](https://dotnet.microsoft.com/download)  
- **SQL Server LocalDB** (Windows) **or** a SQL Server Docker container  
- **Git** and a terminal (PowerShell, bash, etc.)

---

### Installation & Configuration

1. **Clone the repository**  
   ```bash
   git clone https://github.com/YourUsername/Task-Manager-API.git
   cd Task-Manager-API
   ```

2. **Restore dependencies**

   ```bash
   dotnet restore
   ```

3. **Configure the connection string**
   Edit `src/TaskManager.Api/appsettings.json` and set:
   ```jsonc
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskManagerDb;Trusted_Connection=True;"
     }
   }
   ```

---

### Database Migrations

> The API auto-applies migrations on startup, but you can run them manually:

```bash
cd src/TaskManager.Api
dotnet ef database update
```

This creates the `TaskManagerDb` and the `Tasks` table.

---
## ▶️ Running the App
### Local: `dotnet run`

```bash
cd TaskManager.Api
dotnet run
```

You’ll see output like:

```
Now listening on: http://localhost:5159
Now listening on: https://localhost:7159
```

Use those exact ports in the next steps.

---

### Interactive Docs: Swagger UI

Open in your browser:

```
http://localhost:5159/swagger
```

Click **“Try it out”** to explore every endpoint.

---

### REST Client & cURL

* **PowerShell**

  ```powershell
  Invoke-RestMethod http://localhost:5159/api/tasks -UseBasicParsing
  ```

* **cURL**

  ```bash
  curl http://localhost:5159/api/tasks
  ```

* **VS Code `.http` file** (with REST Client extension)

  ```http
  @host = http://localhost:5159

  GET {{host}}/api/tasks
  Accept: application/json
  ```

  Click **“Send Request”**.
---

## 🧪 Testing

### Unit Tests

```bash
cd TaskManager.Tests
dotnet test --filter Category=Unit
```

Mocks the repository to verify `TaskService` behaviors.

---

## 🤝 Contributing

1. **Fork** the repo
2. **Create** a feature branch (`git checkout -b feat/your-feature`)
3. **Commit** your changes (`git commit -m "feat: add your-feature"`)
4. **Push** (`git push origin feat/your-feature`)
5. **Open** a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.
