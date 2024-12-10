using System.Globalization;
using System.Web;

namespace OM.Library;

public class WeatherDataFilter
{
    public double Latitude { get; set; } = 51.7883;
    public double Longitude { get; set; } = 6.1387;
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string Hourly { get; set; } = "temperature_2m,relativehumidity_2m,dewpoint_2m,apparent_temperature,precipitation,rain,snowfall,weathercode,cloudcover,cloudcover_low,cloudcover_mid,cloudcover_high,shortwave_radiation,direct_radiation,diffuse_radiation,direct_normal_irradiance,windspeed_10m,windspeed_100m,windgusts_10m,et0_fao_evapotranspiration,vapor_pressure_deficit";
    public string Timezone { get; set; } = "Europe%2FBerlin";

    public WeatherDataFilter() {}

    public WeatherDataFilter(DateOnly day)
    {
        StartDate = day;
        EndDate = day;
    }

    public string ToQueryString()
    {
        var queryParams = HttpUtility.ParseQueryString(query: string.Empty);
        queryParams.Add(name: nameof(Latitude).ToLower(), value: Latitude.ToString(provider: CultureInfo.InvariantCulture));
        queryParams.Add(name: nameof(Longitude).ToLower(), value: Longitude.ToString(provider: CultureInfo.InvariantCulture));
        if (StartDate != null && EndDate != null)
        {
            queryParams.Add(name: "start_date", value: StartDate.Value.ToString("yyyy-MM-dd"));
            queryParams.Add(name: "end_date", value: EndDate.Value.ToString("yyyy-MM-dd"));
        }
        queryParams.Add(name: nameof(Hourly).ToLower(), value: Hourly);
        queryParams.Add(name: nameof(Timezone).ToLower(), value: Timezone);
        return $"?{HttpUtility.UrlDecode(queryParams.ToString()!)}";
    }
}
