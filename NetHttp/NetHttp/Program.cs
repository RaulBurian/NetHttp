using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using NetHttp;

var indented = new JsonSerializerOptions { WriteIndented = true };
var camelCase = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

await SocketHttp.Examples();
// await WebRequestEx.Examples();

// var httpClient = new HttpClient();
// var response = await httpClient.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);
// Console.WriteLine(JsonSerializer.Serialize(response, indented));

// ------- factory

// var services = new ServiceCollection()
//     .AddHttpClient();
// var sp = services.BuildServiceProvider();
//
// var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
// var factoryClient = httpClientFactory.CreateClient();
// var response = await factoryClient.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);
// Console.WriteLine(JsonSerializer.Serialize(response, indented));

// ------- factory named clients
//
// var services = new ServiceCollection();
// services.AddHttpClient("One", client => { client.BaseAddress = new Uri("https://localhost:7063"); });
// services.AddHttpClient("Two", client => { client.BaseAddress = new Uri("http://localhost:5099"); });
//
// var sp = services.BuildServiceProvider();
// var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
//
// var client1 = httpClientFactory.CreateClient("One");
// var client2 = httpClientFactory.CreateClient("Two");
//
// Console.WriteLine(client1.BaseAddress);
// Console.WriteLine(client2.BaseAddress);

// ---------- factory typed clients -- are transient always
// var services = new ServiceCollection();
// services.AddHttpClient<TypedClientOne>(client => { client.BaseAddress = new Uri("https://localhost:7063"); });
// services.AddHttpClient<TypedClientTwo>(client => { client.BaseAddress = new Uri("http://localhost:5099"); });
//
// var sp = services.BuildServiceProvider();
//
// var client1 = sp.GetRequiredService<TypedClientOne>();
// var client2 = sp.GetRequiredService<TypedClientTwo>();
//
// Console.WriteLine(client1.BaseUrl);
// Console.WriteLine(client2.BaseUrl);

// ----------------------- handler chain
// var httpClient = new HttpClient(new HttpHandlerFirst(new HttpHandlerSecond(new HttpHandlerThird(new SocketsHttpHandler()))));
// var response = await httpClient.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);

// var services = new ServiceCollection();
// services.AddTransient<HttpHandlerFirst>();
// services.AddTransient<HttpHandlerSecond>();
// services.AddTransient<HttpHandlerThird>();
// services.AddHttpClient("One", client => { client.BaseAddress = new Uri("https://localhost:7063"); })
//     .AddHttpMessageHandler<HttpHandlerFirst>()
//     .AddHttpMessageHandler<HttpHandlerSecond>()
//     .AddHttpMessageHandler<HttpHandlerThird>();
//
// var sp = services.BuildServiceProvider();
// var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
//
// var clientOne = httpClientFactory.CreateClient("One");
//
// await clientOne.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);

// ----------------------- handler chain lifetime
// var services = new ServiceCollection();
// services.AddTransient<GuidHandler>();
// services.AddTransient<GuidProvider>();
// services.AddHttpClient("One", client => { client.BaseAddress = new Uri("https://localhost:7063"); })
//     .AddHttpMessageHandler<GuidHandler>();
//
// var sp = services.BuildServiceProvider();
// var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
//
// var clientOne = httpClientFactory.CreateClient("One");
//
// await clientOne.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);
// await clientOne.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);
//
// var clientOneNew = httpClientFactory.CreateClient("One");
//
// await clientOneNew.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);
// await clientOneNew.GetFromJsonAsync<WeatherForecast[]>("https://localhost:7036/weatherforecast", camelCase);

var httpClient = new HttpClient();
await HttpClientEx.StreamExample(httpClient);
