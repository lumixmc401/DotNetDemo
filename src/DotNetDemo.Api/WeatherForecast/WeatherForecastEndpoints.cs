using DotNetDemo.Application.Abstractions;
using DotNetDemo.Application.WeatherForecast;

namespace DotNetDemo.Api.WeatherForecast;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", async (IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
            await dispatcher.SendAsync(new GetWeatherForecastQuery(), cancellationToken))
            .WithName("GetWeatherForecast");

        return app;
    }
}
