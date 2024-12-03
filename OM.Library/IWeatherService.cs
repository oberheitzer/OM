namespace OM.Library;

public interface IWeatherService
{
    Task GetHistoryWeatherAsync(DateOnly day);

    Task GetForecastAsync();
}
