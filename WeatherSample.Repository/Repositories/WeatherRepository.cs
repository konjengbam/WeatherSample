using Microsoft.EntityFrameworkCore;
using WeatherSample.Repository.Entities;
using WeatherSample.Repository.Repositories.Interfaces;

namespace WeatherSample.Repository.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly WeatherDbContext _context;

    public WeatherRepository(WeatherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await _context.WeatherForecasts.ToListAsync();
    }

    public async Task<WeatherForecast?> GetByIdAsync(int id)
    {
        return await _context.WeatherForecasts.FindAsync(id);
    }

    public async Task AddAsync(WeatherForecast forecast)
    {
        await _context.WeatherForecasts.AddAsync(forecast);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(IList<WeatherForecast> forecasts) 
    {
        await _context.WeatherForecasts.AddRangeAsync(forecasts);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WeatherForecast forecast)
    {
        _context.WeatherForecasts.Update(forecast);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var forecast = await _context.WeatherForecasts.FindAsync(id);
        if (forecast != null)
        {
            _context.WeatherForecasts.Remove(forecast);
            await _context.SaveChangesAsync();
        }
    }
}
