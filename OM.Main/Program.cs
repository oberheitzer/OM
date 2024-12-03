using Microsoft.Extensions.DependencyInjection;
using OM.Domain;
using OM.Library;

namespace OM.Main;

internal class Program
{
    static async Task Main(string[] args)
    {
        var provider = Starter.Run();

        if (args.Length > 0)
        {
            var weatherService = provider.GetRequiredService<IWeatherService>();
            var storeService = provider.GetRequiredService<IStoreService>();
            if (args[0] == "-f")
            {
                WeatherData data = await weatherService.GetForecastAsync();
                storeService.Save(data: data, folder: "forecast");
            }
            else if (args[0] == "-s" && args.Length == 2 && DateOnly.TryParse(args[1], out DateOnly day))
            {
                WeatherData data = await weatherService.GetHistoryWeatherAsync(day: day);
                storeService.Save(data: data, folder: "actual");
            }
            else
            {
                Console.WriteLine("Invalid arguments.");
            }
        }
        else
        {
            Console.WriteLine("Missing arguments.");
        }
    }
}