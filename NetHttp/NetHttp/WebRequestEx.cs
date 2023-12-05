using System.Net;
using System.Text.Json;

namespace NetHttp;

public static class WebRequestEx
{
    public static async Task Examples()
    {
        var indented = new JsonSerializerOptions { WriteIndented = true };
        var camelCase = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var webRequest = WebRequest.CreateHttp("https://localhost:7036/weatherforecast");
        var webResponse = await webRequest.GetResponseAsync();
        var resultWebRequest = await JsonSerializer.DeserializeAsync<WeatherForecast[]>(webResponse.GetResponseStream(), camelCase);

        Console.WriteLine(JsonSerializer.Serialize(resultWebRequest , indented));
    }
}
