using Microsoft.Extensions.DependencyInjection;

namespace OM.Main;

public static class Starter
{
    public static ServiceProvider Run()
    {
        var services = new ServiceCollection();
        services.AddServices();
        return services.BuildServiceProvider();
    }
}
