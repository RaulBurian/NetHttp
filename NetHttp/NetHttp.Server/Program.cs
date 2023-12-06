using Microsoft.AspNetCore.Mvc;
using NetHttp;
using NetHttp.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<GuidProvider>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<Handler>();
builder.Services.AddHttpClient("Mario")
    .AddHttpMessageHandler<Handler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseMiddleware<ResponseDeChunker>();

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.MapGet("/weatherforecastStream",  () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();

    return AsyncEnumerableReturn(forecast);
});

app.MapGet("/handler", async ([FromServices] IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("Mario");

    await client.GetAsync("https://localhost:7036/weatherforecast");
    await client.GetAsync("https://localhost:7036/weatherforecast");
});

app.Run();

async IAsyncEnumerable<WeatherForecast> AsyncEnumerableReturn(WeatherForecast[] weatherForecasts)
{
    foreach (var weatherForecast in weatherForecasts)
    {
        yield return weatherForecast;

        await Task.Delay(1000);
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
