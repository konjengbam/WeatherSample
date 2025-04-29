using WeatherSample.ServiceDefaults.DTOs;

namespace WeatherSample.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecastDto[]> GetWeatherAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<WeatherForecastDto>? forecasts = null;

        await foreach (var forecast in FetchWeatherForecastsAsync(cancellationToken))
        {
            if (forecasts?.Count >= maxItems)
            {
                break;
            }
            if (forecast is not null)
            {
                forecasts ??= [];
                forecasts.Add(forecast);
            }
        }

        return forecasts?.ToArray() ?? [];

        IAsyncEnumerable<WeatherForecastDto?> FetchWeatherForecastsAsync(CancellationToken cancellationToken)
        {
            return httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecastDto>("/api/v1/weatherforecast", cancellationToken);
        }
    }
}
