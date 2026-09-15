# User Management API
A simple REST API for managing users, built with ASP.NET Core. This project was developed with assistance from GitHub Copilot for writing, debugging, and refactoring code.

This API supports full CRUD operations, input validation, structured request logging, and API-key based authentication.

# Features
Full **CRUD** endpoints for users (GET, POST, PUT, DELETE)
**Input validation**  using data annotations
**Request logging middleware** that logs method, path, status code, and elapsed time
**Authentication middleware** enforcing a valid API key (x-api-key header)
**Global exception handling** for clean, consistent error responses
**In-memory** data store (seeds sample users on startup)
**Swagger / OpenAPI UI** for easy testing
**Copilot-assisted** debugging notes (see below)
# Tech Stack
.NET 8 / ASP.NET Core
C#
Swagger (Swashbuckle)
In-memory storage (no external database required)
# Repository Structure
UserManagementApi/
├── Controllers/
│   └── UserController.cs        # CRUD endpoints
├── Middleware/
│   ├── RequestLoggingMiddleware.cs   # Logs each request
│   └── ApiKeyMiddleware.cs           # Validates x-api-key header
├── Models/
│   └── User.cs                  # User model + validation attributes
├── Data/
│   └── UserStore.cs             # In-memory user store (seed data)
├── Program.cs                   # App pipeline & service registration
├── appsettings.json             # API key & logging config
└── README.md
# Getting Started
Prerequisites
.NET 8 SDK
Run the API
git clone https://github.com/<your-username>/UserManagementApi.git
cd UserManagementApi
dotnet run
The API starts on https://localhost:5001 (see launchSettings.json). Swagger UI is available at https://localhost:5001/swagger.

# Authentication
All endpoints require a valid API key sent in the x-api-key header.

x-api-key: my-secret-api-key
The expected key is stored in appsettings.json under ApiKey. Requests without a valid key receive 401 Unauthorized.

# API Endpoints
**Method**	**Route**     	**Description**
GET          /api/users     	Get all users
GET	        /api/users/{id}	  Get a user by id
POST	     /api/users	        Create a new user
PUT	      /api/users/{id}    	Update an existing user
DELETE  	/api/users/{id}	    Delete a user
# Example: Create a user
POST /api/users
x-api-key: my-secret-api-key
Content-Type: application/json

{
  "name": "Appu Sahu",
  "email": "appu@example.com",
  "age": 25
}
# Example response
{
  "id": 1,
  "name": "Appu Sahu",
  "email": "appu@example.com",
  "age": 25
}
# Validation
The User model enforces the following rules:

**Field**    	**Attribute**	                    **Rule**
Name	         [Required], [StringLength]       	Required, 2–50 characters
Email	         [Required], [EmailAddress]        	Required, valid email format
Age	           [Range(0, 120)]	                  Between 0 and 120
Invalid input returns 400 Bad Request with a list of validation errors.

# Middleware
# Request Logging
RequestLoggingMiddleware logs every request with method, path, status code, and elapsed milliseconds.

[Request] GET /api/users -> 200 OK in 12 ms
# API-Key Authentication
ApiKeyMiddleware reads the x-api-key header and compares it to the configured value. Invalid or missing keys are rejected before reaching the controller.

# Debugging with GitHub Copilot
During development, Copilot helped identify and fix a **null-reference bug** in **UpdateUser:** the PUT handler tried to update a user before checking whether it existed. Copilot suggested a null check that returns 404 Not Found before performing the update:

if (existingUser is null)
    return NotFound($"User with id {id} was not found.");

existingUser.Name = updatedUser.Name;
existingUser.Email = updatedUser.Email;
existingUser.Age = updatedUser.Age;
Copilot was also used to refactor validation logic and generate XML documentation comments.

# Testing
Test the endpoints using:

**Swagger UI** at /swagger (easiest)
**curl:**
curl -H "x-api-key: my-secret-api-key" https://localhost:5001/api/users
**Postman:** set the x-api-key header on every request
# License
This project is for educational purposes as part of the Building a Simple API with Copilot assignment.

# Acknowledgments
This project was developed with assistance from **GitHub Copilot** for writing, debugging, and refactoring code.
