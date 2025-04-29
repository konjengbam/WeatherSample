namespace WeatherSample.Repository.Entities;

public record WeatherForecast
{
    public int Id { get; init; } // Primary key for EF
    public DateOnly Date { get; init; }
    public int TemperatureC { get; init; }
    public string? Summary { get; init; }
}