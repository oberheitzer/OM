namespace OM.Domain;

public class WeatherData
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double GenerationtimeMs { get; set; }
    public int UtcOffsetSeconds { get; set; }
    public string Timezone { get; set; } = string.Empty;
    public string TimezoneAbbreviation { get; set; } = string.Empty;
    public double Elevation { get; set; }
    public HourlyUnits HourlyUnits { get; set; } = null!;
    public HourlyData Hourly { get; set; } = null!;
}
