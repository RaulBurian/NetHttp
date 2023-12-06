using System.Text.Json.Serialization;

namespace NetHttp;

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary);

[JsonSerializable(typeof(WeatherForecast))]
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class WeatherForecastContext : JsonSerializerContext
{

}

[JsonSerializable(typeof(WeatherForecast[]))]
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class WeatherForecastArrayContext : JsonSerializerContext
{

}
