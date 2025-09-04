# CelebApi (.NET Backend)

This is the backend API for the Celebrity Full Stack application, built using ASP.NET Core Web API.

## Features
- Exposes endpoints to manage celebrities
- Supports CRUD operations
- Stores data in JSON files
- Thread-safe implementation
- CORS configured for frontend access

## Setup
1. Ensure you have [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) installed.
2. Open the solution in Visual Studio or your preferred IDE.
3. Restore packages and build the solution:
   ```bash
   dotnet restore
   dotnet build
   ```
4. Run the API:
   ```bash
   cd CelebApi
   dotnet run
   ```

## Endpoints
- `GET /api/celebs/v1/celebs`: Get all celebrities
- `GET /api/celebs/v1/{id}`: Get a celebrity by a specific id.
- `ADD /api/celebs/v1/celeb`: Add a celeb to the Json file.
- `PUT /api/celeb/{id}`: Update a celeb by a given valid id.
- `DELETE /api/celebs/v1/celebs/{name}`: Delete a celebrity by name
- `POST /api/celebs/v1/celebs/reset`: Reset to original data

## Configuration
Edit `appsettings.json` for file paths and CORS configuration.

