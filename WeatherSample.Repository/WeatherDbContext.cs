using Microsoft.EntityFrameworkCore;
using WeatherSample.Repository.Entities;

namespace WeatherSample.Repository;

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}
