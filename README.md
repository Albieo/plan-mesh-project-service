# Project Service

ASP.NET Core 8 API for managing projects, features, and user stories in the
project planning tool.

## Prerequisites

- .NET 8 SDK
- PostgreSQL
- A JWT signing secret that matches the auth service

## Run locally

```bash
dotnet restore
dotnet run
```

The default local URL is `http://localhost:5279`.

Swagger is available in development at `http://localhost:5279/swagger`.

## Endpoints

All project, feature, and user story endpoints require a bearer token.

- `GET /health`
- `GET /`
- `GET /projects`
- `GET /projects/{id}`
- `POST /projects`
- `PUT /projects/{id}`
- `DELETE /projects/{id}`
- `GET /projects/{projectId}/features`
- `GET /projects/{projectId}/features/{id}`
- `POST /projects/{projectId}/features`
- `PUT /projects/{projectId}/features/{featureId}`
- `DELETE /projects/{projectId}/features/{featureId}`
- `GET /projects/{projectId}/features/{featureId}/user-stories`
- `GET /projects/{projectId}/features/{featureId}/user-stories/{id}`
- `POST /projects/{projectId}/features/{featureId}/user-stories`
- `PUT /projects/{projectId}/features/{featureId}/user-stories/{userStoryId}`
- `DELETE /projects/{projectId}/features/{featureId}/user-stories/{userStoryId}`

## Configuration

Set the PostgreSQL connection string with `ConnectionStrings__DefaultConnection`.
Set the JWT signing secret with `Jwt__Secret`.

For local development, export both values before running the service:

```bash
export ConnectionStrings__DefaultConnection='Host=localhost;Port=5432;Database=projectservice;Username=<user>;Password=<password>'
export Jwt__Secret='<same value as JWT_SECRET used by auth-service>'
dotnet run
```

The service creates the database schema on startup with Entity Framework Core
`EnsureCreatedAsync`.
