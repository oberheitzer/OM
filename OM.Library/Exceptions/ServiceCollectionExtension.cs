using OM.Library;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddServices(this ServiceCollection services)
    {
        services.AddHttpClient<IWeatherService, WeatherService>(httpClient => {
            // httpClient.BaseAddress = new Uri(uriString: Constant.BaseUrl);
        });
        services.AddScoped<IStoreService, StoreService>();
        return services;
    }
}
