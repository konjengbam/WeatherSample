using WeatherSample.ServiceDefaults.DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using Swashbuckle.AspNetCore.Annotations;
using WeatherSample.Repository.Entities;
using WeatherSample.Repository.Repositories.Interfaces;

namespace WeatherSample.ApiService.Controller;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")] // Specify version 1.0
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly IFeatureManager _featureManager;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(IWeatherRepository weatherRepository,
                                     IFeatureManager featureManager,
                                     ILogger<WeatherForecastController> logger)
    {
        _weatherRepository = weatherRepository;
        _featureManager = featureManager;
        _logger = logger;

        // Trigger asynchronous seeding in a non-blocking way
        PopulateDatabaseAsync().GetAwaiter().GetResult();
        _logger = logger;

    }

    /// <summary>
    /// Retrieves all weather forecasts.
    /// </summary>
    /// <returns>A list of weather forecasts.</returns>
    [HttpGet]
    [SwaggerOperation(Summary = "Get all weather forecasts", Description = "Retrieves all weather forecasts from the database.")]
    [SwaggerResponse(200, "Successfully retrieved weather forecasts", typeof(IEnumerable<WeatherForecastDto>))]
    [SwaggerResponse(404, "No weather forecasts found")]
    public async Task<ActionResult<IEnumerable<WeatherForecastDto>>> Get()
    {
        var forecasts = await _weatherRepository.GetAllAsync();
        if (forecasts == null || !forecasts.Any())
        {
            return NotFound("No weather forecasts found.");
        }
        return Ok(forecasts.Adapt<IEnumerable<WeatherForecastDto>>());
    }

    /// <summary>
    /// Creates a new weather forecast.
    /// </summary>
    /// <param name="forecastDto">The weather forecast to create.</param>
    /// <returns>The created weather forecast.</returns>
    [HttpPost]
    [SwaggerOperation(Summary = "Create a new weather forecast", Description = "Adds a new weather forecast to the database.")]
    [SwaggerResponse(201, "Successfully created weather forecast", typeof(WeatherForecastDto))]
    [SwaggerResponse(400, "Validation failed")]
    public async Task<ActionResult<WeatherForecastDto>> Post(WeatherForecastDto forecastDto)
    {
        _logger.LogInformation("Received WeatherForecastDto: {@ForecastDto}", forecastDto);

        // Check if the ModelState is valid
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var forecast = forecastDto.Adapt<WeatherForecast>();
        await _weatherRepository.AddAsync(forecast);

        _logger.LogInformation("Weather forecast created with ID: {Id}", forecast.Id);
        return CreatedAtAction(nameof(Get), new { id = forecast.Id }, null);
    }

    private async Task PopulateDatabaseAsync()
    {
        try
        {
            if (await _featureManager.IsEnabledAsync("EnableDatabaseSeeding"))
            {
                // TODO: Improve this for production
                var weatherForecastAll = await _weatherRepository.GetAllAsync();
                if (!weatherForecastAll.Any())
                {
                    await SeedDatabaseAsync();
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while populating the database.");
        }

    }

    private async Task SeedDatabaseAsync()
    {
        string[] summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

        var forecasts = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToList();

        await _weatherRepository.AddRangeAsync(forecasts); 
    }
}
