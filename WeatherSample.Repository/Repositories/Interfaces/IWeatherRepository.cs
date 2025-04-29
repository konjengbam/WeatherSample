using WeatherSample.Repository.Entities;

namespace WeatherSample.Repository.Repositories.Interfaces;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
    Task<WeatherForecast?> GetByIdAsync(int id);
    Task AddAsync(WeatherForecast forecast);
    Task AddRangeAsync(IList<WeatherForecast> forecasts); 
    Task UpdateAsync(WeatherForecast forecast);
    Task DeleteAsync(int id);
}
