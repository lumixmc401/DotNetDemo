using MediatR;

namespace DotNetDemo.Api.Features.WeatherForecast;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", async (IMediator mediator) =>
            await mediator.Send(new GetWeatherForecastQuery()))
            .WithName("GetWeatherForecast");

        return app;
    }
}
