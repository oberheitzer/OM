using OM.Domain;

namespace OM.Library;

public interface IWeatherService
{
    Task<WeatherData> GetHistoryWeatherAsync(DateOnly day);

    Task<WeatherData> GetForecastAsync();
}
