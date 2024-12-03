namespace OM.Domain;

public class HourlyData
{
    public List<string> Time { get; set; } = [];
    public List<double> Temperature2m { get; set; } = [];
    public List<int> Relativehumidity2m { get; set; } = [];
    public List<double> Dewpoint2m { get; set; } = [];
    public List<double> ApparentTemperature { get; set; } = [];
    public List<double> Precipitation { get; set; } = [];
    public List<double> Rain { get; set; } = [];
    public List<double> Snowfall { get; set; } = [];
    public List<int> Weathercode { get; set; } = [];
    public List<int> Cloudcover { get; set; } = [];
    public List<int> CloudcoverLow { get; set; } = [];
    public List<int> CloudcoverMid { get; set; } = [];
    public List<int> CloudcoverHigh { get; set; } = [];
    public List<double> ShortwaveRadiation { get; set; } = [];
    public List<double> DirectRadiation { get; set; } = [];
    public List<double> DiffuseRadiation { get; set; } = [];
    public List<double> DirectNormalIrradiance { get; set; } = [];
    public List<double> Windspeed10m { get; set; } = [];
    public List<double> Windspeed100m { get; set; } = [];
    public List<double> Windgusts10m { get; set; } = [];
    public List<double> Et0FaoEvapotranspiration { get; set; } = [];
    public List<double> VaporPressureDeficit { get; set; } = [];
}
