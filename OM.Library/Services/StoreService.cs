using OM.Domain;
using Parquet;
using Parquet.Data;
using Parquet.Schema;

namespace OM.Library;

internal sealed class StoreService : IStoreService
{
    public async void Save(WeatherData data, string folder)
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        string partitionedPath = Path.Combine(folder, $"{currentDate.Year}-{currentDate.Month}-{currentDate.Day}");
        Directory.CreateDirectory(partitionedPath);

        var fields = new List<DataField>
        {
            new DataField<string>("time"),
            new DataField<double>("temperature"),
        };

        var schema = new ParquetSchema(fields);

        var timeColumn = new DataColumn(field: fields[0], data: data.Hourly.Time.ToArray());
        var temperatureColumn = new DataColumn(field: fields[1], data: data.Hourly.Temperature2m.ToArray());

        string parquetFilePath = Path.Combine(partitionedPath, $"{folder}.parquet");

        using var fileStream = File.Create(parquetFilePath);
        using var writer = await ParquetWriter.CreateAsync(schema, fileStream);
        using ParquetRowGroupWriter groupWriter = writer.CreateRowGroup();
        await groupWriter.WriteColumnAsync(timeColumn);
        await groupWriter.WriteColumnAsync(temperatureColumn);

    }
}
