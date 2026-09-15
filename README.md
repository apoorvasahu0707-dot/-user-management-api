# User Management API

A simple ASP.NET Core Web API for managing users, built with the help of GitHub Copilot
(code generation, debugging suggestions, and middleware implementation).

## Features

- **CRUD endpoints** for users: `GET`, `POST`, `PUT`, `DELETE`
- **Validation**: `[Required]`, `[EmailAddress]`, `[StringLength]`, `[Range]` on the `User` model,
  plus a custom check that rejects duplicate emails
- **Middleware**:
  - `RequestLoggingMiddleware` – logs method, path, status code, and response time for every request
  - `ApiKeyMiddleware` – simple authentication; every request (except `/swagger`) needs header
    `X-Api-Key: mysecretkey123`

## Project structure

```
UserApi/
├── Controllers/
│   └── UsersController.cs
├── Middleware/
│   ├── RequestLoggingMiddleware.cs
│   └── ApiKeyMiddleware.cs
├── Models/
│   └── User.cs
├── Program.cs
├── appsettings.json
└── UserApi.csproj
```

## How to run

```bash
dotnet restore
dotnet run
```

Then open `https://localhost:<port>/swagger` in your browser to test endpoints interactively.

## API Endpoints

| Method | Route              | Description          |
|--------|---------------------|-----------------------|
| GET    | /api/users          | Get all users         |
| GET    | /api/users/{id}     | Get user by id        |
| POST   | /api/users          | Create a new user     |
| PUT    | /api/users/{id}     | Update existing user  |
| DELETE | /api/users/{id}     | Delete a user          |

All requests (except Swagger) must include header:
```
X-Api-Key: mysecretkey123
```

### Sample POST body

```json
{
  "name": "Apoorva Sahu",
  "email": "apoorva@example.com",
  "age": 22
}
```

## Debugging with Copilot

During development, Copilot was used to:
- Suggest fixes for a null-reference bug in `UpdateUser` when the id didn't exist
- Refactor duplicate validation logic into `ModelState.IsValid` checks
- Generate the boilerplate for `RequestLoggingMiddleware` and refine the log message format
