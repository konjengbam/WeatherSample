using WeatherSample.ServiceDefaults.DTOs;
using Mapster;
using WeatherSample.Repository.Entities;

namespace WeatherSample.ApiService
{
    public static class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<WeatherForecast, WeatherForecastDto>
                .NewConfig()
                .Map(dest => dest.TemperatureF, src => 32 + (int)(src.TemperatureC / 0.5556));
        }
    }
}
