using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherSample.ApiService.Controller;
using WeatherSample.ApiService.Validators;
using WeatherSample.Repository.Entities;
using WeatherSample.Repository.Repositories.Interfaces;
using WeatherSample.ServiceDefaults.DTOs;

namespace WeatherSample.ApiService.Tests
{
    public class WeatherForecastControllerTests
    {
        private readonly Mock<IWeatherRepository> _mockWeatherRepository;
        private readonly Mock<ILogger<WeatherForecastController>> _mockLogger;
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            _mockWeatherRepository = new Mock<IWeatherRepository>();
            _mockLogger = new Mock<ILogger<WeatherForecastController>>();

            // Initialize the controller with mocked dependencies
            _controller = new WeatherForecastController(
                _mockWeatherRepository.Object,
                Mock.Of<Microsoft.FeatureManagement.IFeatureManager>(), // Mock feature manager
                _mockLogger.Object
            );
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithWeatherForecasts()
        {
            // Arrange
            var forecasts = new List<WeatherForecast>
            {
                new WeatherForecast { Id = 1, Date = DateOnly.FromDateTime(DateTime.Now), TemperatureC = 25, Summary = "Warm" },
                new WeatherForecast { Id = 2, Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)), TemperatureC = 30, Summary = "Hot" }
            };
            _mockWeatherRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(forecasts);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<WeatherForecastDto>>(okResult.Value);
            Assert.Equal(2, returnValue.Count());
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenNoForecastsExist()
        {
            // Arrange
            _mockWeatherRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<WeatherForecast>());

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenSummaryIsLong()
        {
            // Arrange
            var forecastDto = new WeatherForecastDto
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = new string('A', 201) // Simulate a long summary exceeding the allowed limit
            };

            var validator = new WeatherForecastDtoValidator();
            var validationResult = await validator.ValidateAsync(forecastDto);

            if (!validationResult.IsValid)
            {
                _controller.ModelState.AddModelError("Summary", validationResult.Errors.First().ErrorMessage);
            }

            // Act
            var result = await _controller.Post(forecastDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
        }



        [Fact]
        public async Task Post_ReturnsCreatedAtActionResult_WhenForecastIsValid()
        {
            // Arrange
            var forecastDto = new WeatherForecastDto
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                TemperatureC = 20,
                Summary = "Cool"
            };
            var forecast = new WeatherForecast
            {
                Id = 1,
                Date = forecastDto.Date,
                TemperatureC = forecastDto.TemperatureC,
                Summary = forecastDto.Summary
            };

            _mockWeatherRepository.Setup(repo => repo.AddAsync(It.IsAny<WeatherForecast>()))
                                  .Callback<WeatherForecast>(f =>
                                  {
                                      f.Id = forecast.Id;
                                  })
                                  .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(forecastDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.Get), createdAtActionResult.ActionName);
        }
    }
}
