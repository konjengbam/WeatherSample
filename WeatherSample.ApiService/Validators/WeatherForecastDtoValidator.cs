using WeatherSample.ServiceDefaults.DTOs;
using FluentValidation;

namespace WeatherSample.ApiService.Validators;

public class WeatherForecastDtoValidator : AbstractValidator<WeatherForecastDto>
{
    public WeatherForecastDtoValidator()
    {
        RuleFor(x => x.Date)
           .NotEmpty().WithMessage("The Date field is required. Please provide a valid date.");

        RuleFor(x => x.TemperatureC)
            .InclusiveBetween(-20, 55)
            .WithMessage("The TemperatureC must be between -20 and 55 degrees Celsius.");

        RuleFor(x => x.Summary)
            .MaximumLength(20)
            .WithMessage("The Summary must not exceed 20 characters.");
    }
}
