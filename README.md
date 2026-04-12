# EmployeeManager — Run with Docker

This repository contains the `EmployeeManager.WebAPI` ASP.NET Core backend. The project includes a `Dockerfile` and `docker-compose.yml` to run the API together with a SQL Server database for local development.

Prerequisites
- Docker Desktop (Windows) or Docker Engine + Docker Compose
- PowerShell (recommended) or any shell

Quick start (development)
1. Open a terminal at the repository root (for example PowerShell):

   ```powershell
   cd "D:\WORK\Backend\C#\EmployeeManager"
   ```

2. Make sure the JWT secret and the SA password in `docker-compose.yml` are set to secure values before using in any shared environment. By default the compose file contains placeholders:
   - `SA_PASSWORD` for SQL Server
   - `JsonWebToken__SecretKey` for the API

   You can override them at runtime using environment files or CLI environment variables. Example (PowerShell):

   ```powershell
   $env:SA_PASSWORD = 'Your_strong_P@ssw0rd'
   $env:JsonWebToken__SecretKey = 'YourJWTSecretKeyHere'
   docker compose up --build
   ```

3. Build and start services with Docker Compose:

   ```powershell
   docker compose up --build
   ```

   - The SQL Server container will start and expose port `1433` on the host.
   - The Web API container will be available at `http://localhost:5000` (mapped from container port 80).

4. Open the API (Swagger / OpenAPI) in your browser:
   - `http://localhost:5000/swagger` (if your application exposes Swagger in Development environment)

Notes and tips
- The `webapi` service in `docker-compose.yml` mounts the local `EmployeeManager.WebAPI` folder into the container (`./EmployeeManager.WebAPI:/app`). This enables fast iterative development (code changes reflected without rebuilding the image) but should be removed for production image runs.
- For production deployments, do not mount source code and do not store secrets in `docker-compose.yml`. Use environment secret stores or CI/CD pipeline secrets.
- The SQL Server data is persisted using a Docker named volume `sqlserver-data`. To remove persisted data (reset DB), stop containers and remove the volume:

  ```powershell
  docker compose down
  docker volume rm employee_manager_sqlserver-data
  ```

- If you prefer to use a local SQL Server instance instead of the containerized one, update `ConnectionStrings__DefaultConnection` in the environment section of `docker-compose.yml` or set the environment variable before starting.

Troubleshooting
- If the SQL Server container fails to become healthy, check logs:
  ```powershell
  docker logs employee_manager_sql
  ```
- Check the Web API logs for errors:
  ```powershell
  docker logs employee_manager_webapi
  ```

Files added/changed for Docker support
- `EmployeeManager.WebAPI/Dockerfile` — multi-stage Dockerfile to build and run the API
- `docker-compose.yml` — defines `sqlserver` and `webapi` services for local development
- `.dockerignore` — prevents unnecessary files from being sent to the Docker build context

Next recommended steps
- Move secrets out of the repository and into environment variables / secret manager
- Add EF Core migrations and automatic migration execution during startup (or via a migration container)
- Add a small `README` snippet to show sample API requests or include a Postman collection

If you want, I can add a `Makefile` or PowerShell script to simplify common tasks (build, run, stop, reset-db).
