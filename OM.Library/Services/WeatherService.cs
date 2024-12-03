using System.Text.Json;
using OM.Domain;

namespace OM.Library;

internal sealed class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    private const string ForecastBaseUri = "https://api.open-meteo.com/v1/forecast";
    private const string HistoryBaseUri = "https://archive-api.open-meteo.com/v1/archive";

    private const string ForecastQueryParams = "latitude=51.7883&longitude=6.1387&hourly=temperature_2m,relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation,rain,snowfall,weathercode,cloudcover,cloudcover_low,cloudcover_mid,cloudcover_high,shortwave_radiation,direct_radiation,diffuse_radiation,direct_normal_irradiance,windspeed_10m,windspeed_100m,windgusts_10m,et0_fao_evapotranspiration,vapor_pressure_deficit&timezone=Europe%2FBerlin";

    public WeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
    }

    public async Task<WeatherData> GetForecastAsync()
    {
        _httpClient.BaseAddress = new Uri(ForecastBaseUri);
        HttpResponseMessage response = await _httpClient.GetAsync(requestUri: $"?{ForecastQueryParams}");
        if (response.IsSuccessStatusCode)
        {
            return ToWeatherData(json: await response.Content.ReadAsStreamAsync());
        }
        throw new WeatherException(message: $"The request has returned with the status code of {response.StatusCode}");
    }

    public async Task<WeatherData> GetHistoryWeatherAsync(DateOnly day)
    {
        var formattedDay = day.ToString("yyyy-MM-dd");
        _httpClient.BaseAddress = new Uri(HistoryBaseUri);
        
        HttpResponseMessage response = await _httpClient.GetAsync(requestUri: $"?latitude=51.7883&longitude=6.1387&start_date={formattedDay}&end_date={formattedDay}&hourly=temperature_2m,relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation,rain,snowfall,weathercode,cloudcover,cloudcover_low,cloudcover_mid,cloudcover_high,shortwave_radiation,direct_radiation,diffuse_radiation,direct_normal_irradiance,windspeed_10m,windspeed_100m,windgusts_10m,et0_fao_evapotranspiration,vapor_pressure_deficit&timezone=Europe%2FBerlin");
        if (response.IsSuccessStatusCode)
        {
            return ToWeatherData(json: await response.Content.ReadAsStreamAsync());
        }
        throw new WeatherException(message: $"The request has returned with the status code of {response.StatusCode}");
    }

    private WeatherData ToWeatherData(Stream json)
    {
        try
        {
            return JsonSerializer.Deserialize<WeatherData>(utf8Json: json, options: _options) ?? new WeatherData();    
        }
        catch (JsonException exception)
        {
            throw new WeatherException("The deserialization was unsuccessful.", exception: exception);
        }
    }
}
