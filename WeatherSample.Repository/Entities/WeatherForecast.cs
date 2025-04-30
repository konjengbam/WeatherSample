namespace WeatherSample.Repository.Entities;

public class WeatherForecast
{
    public int Id { get; set; } // Primary key for EF
    public required  DateOnly Date { get; set; }
    public required int TemperatureC { get; set; }
    public string? Summary { get; set; }
}