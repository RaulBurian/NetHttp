using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace NetHttp;

public static class HttpClientEx
{
    public static async Task StreamExample(HttpClient httpClient)
    {
        var response = await httpClient.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecastStream", WeatherForecastArrayContext.Default.WeatherForecastArray);

        Console.WriteLine(JsonSerializer.Serialize(response , WeatherForecastArrayContext.Default.WeatherForecastArray!));

        // await foreach (var weatherForecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("https://localhost:7036/weatherforecastStream", WeatherForecastContext.Default.WeatherForecast))
        // {
            // Console.WriteLine(weatherForecast);
        // }
    }
}
