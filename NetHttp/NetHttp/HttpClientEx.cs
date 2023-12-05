using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

namespace NetHttp;

public static class HttpClientEx
{
    public static async Task StreamExample(HttpClient httpClient)
    {
        var indented = new JsonSerializerOptions { WriteIndented = true };
        var camelCase = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var response = await httpClient.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecastStream", camelCase);

        Console.WriteLine(JsonSerializer.Serialize(response , indented));

        await foreach (var weatherForecast in httpClient.GetFromJsonAsAsyncEnumerable<WeatherForecast>("https://localhost:7036/weatherforecastStream", camelCase))
        {
            Console.WriteLine(weatherForecast);
        }
    }
}
