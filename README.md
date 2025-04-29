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
   - Includes database seeding logic for initial data population.

### 2. **WeatherSample.Repository**
   - Implements the repository pattern for database operations.
   - Contains the `WeatherRepository` class for CRUD operations on `WeatherForecast` entities.
   - Includes interfaces like `IWeatherRepository` for abstraction.

### 3. **WeatherSample.Web**
   - The Blazor frontend project.
   - Demonstrates the use of distributed cache via Redis (need redis-server running on Docker/WSL)
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
   git clone https://github.com/your-repo/WeatherSample.git cd WeatherSample

2. **Configure the Database**
   - Update the connection string in `appsettings.json` for the API project (`WeatherSample.ApiService`).

3. **Apply Migrations**
   Run the following command to create the database and apply migrations:
   dotnet ef database update --project WeatherSample.Repository --startup-project WeatherSample.ApiService

4. **Run the Application**
   Start the API and Blazor projects:
   dotnet run --project WeatherSample.ApiService dotnet run --project WeatherSample.Web

5. **Access the Application**
   - API: Navigate to `https://localhost:5001/swagger` for API documentation.
   - Blazor Frontend: Navigate to `https://localhost:5002`.

---

## API Endpoints

### WeatherForecastController

- **GET** `/api/v1.0/WeatherForecast`
  - Retrieves all weather forecasts.
  - Response: `200 OK` with a list of `WeatherForecastDto`.

- **POST** `/api/v1.0/WeatherForecast`
  - Creates a new weather forecast.
  - Request Body: `WeatherForecastDto`.
  - Response: `201 Created`.

---

## Technologies Used

- **Frontend**: Blazor
- **Backend**: ASP.NET Core Web API
- **Database**: Entity Framework Core with SQL Server
- **Dependency Injection**: Built-in .NET DI
- **Feature Management**: `Microsoft.FeatureManagement`
- **API Documentation**: Swagger (Swashbuckle)

---

## Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Submit a pull request with a detailed description of your changes.

---

## License

This project is licensed under the [MIT License](LICENSE).

---

## Acknowledgments

- [Microsoft Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [Swagger](https://swagger.io/)   
