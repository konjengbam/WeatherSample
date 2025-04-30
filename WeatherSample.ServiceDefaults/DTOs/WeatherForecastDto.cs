namespace WeatherSample.ServiceDefaults.DTOs;

public record WeatherForecastDto
{
    public required DateOnly Date { get; init; }
    public required int TemperatureC { get; init; }
    public string? Summary { get; init; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556); // Calculated property
}
