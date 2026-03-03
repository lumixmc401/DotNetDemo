using DotNetDemo.Application.Abstractions;

namespace DotNetDemo.Application.WeatherForecast;

public class GetWeatherForecastHandler : IQueryHandler<GetWeatherForecastQuery, IEnumerable<WeatherForecastDto>>
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public Task<IEnumerable<WeatherForecastDto>> HandleAsync(
        GetWeatherForecastQuery query,
        CancellationToken cancellationToken = default)
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecastDto(
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                Summaries[Random.Shared.Next(Summaries.Length)]
            ))
            .ToArray();

        return Task.FromResult<IEnumerable<WeatherForecastDto>>(forecast);
    }
}
