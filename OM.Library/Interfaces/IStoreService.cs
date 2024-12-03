using OM.Domain;

namespace OM.Library;

public interface IStoreService
{
    void Save(WeatherData data, string folder);
}
