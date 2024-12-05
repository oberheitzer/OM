﻿using OM.Library;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtension
{
    public static ServiceCollection AddServices(this ServiceCollection services)
    {
        services.AddHttpClient<IWeatherService, WeatherService>();
        services.AddScoped<IStoreService, StoreService>();
        return services;
    }
}