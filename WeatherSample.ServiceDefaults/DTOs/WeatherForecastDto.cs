namespace WeatherSample.ServiceDefaults.DTOs;

public record WeatherForecastDto
{
    public required DateOnly Date { get; set; }
    public required int TemperatureC { get; set; }
    public string? Summary { get; set; }
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556); // Calculated property
}
