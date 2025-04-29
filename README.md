# WeatherSample 

WeatherSample is a Blazor-based application designed to manage and display weather forecasts. The solution is built using modern .NET technologies, including .NET 8, Entity Framework Core, and ASP.NET Core Web API. It follows a clean architecture with a repository pattern for database interactions.

---

## Features

- **Blazor Frontend**: A modern, interactive UI built with Blazor.
- **RESTful API**: Provides endpoints for managing weather forecasts.
- **Repository Pattern**: Clean separation of database logic using repositories.
- **Entity Framework Core**: Database access and migrations.
- **Feature Management**: Toggle features dynamically using `Microsoft.FeatureManagement`.
- **Swagger Integration**: API documentation and testing.

---

## Solution Structure

The solution is divided into the following projects:

### 1. **WeatherSample.ApiService**
   - Contains the Web API for managing weather forecasts.
   - Implements controllers like `WeatherForecastController` to handle HTTP requests.
   - Uses dependency injection to interact with the repository layer.
   - Contains the `WeatherRepository` class for CRUD operations on `WeatherForecast` entities.
   - Includes interfaces like `IWeatherRepository` for abstraction.
   - Includes database seeding logic for initial data population.

### 2. **WeatherSample.Repository**
   - Implements the repository pattern for database operations.
   - Contains `WeatherForecast` entities.

### 3. **WeatherSample.Web**
   - The Blazor frontend project.
   - Provides a user-friendly interface for interacting with the weather forecast data.

### 4. **WeatherSample.Shared**
   - Contains shared DTOs and interfaces used across the solution.
   - Ensures consistency between the API and frontend.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server or any other database supported by Entity Framework Core.

### Setup Instructions

1. **Clone the Repository**
   
