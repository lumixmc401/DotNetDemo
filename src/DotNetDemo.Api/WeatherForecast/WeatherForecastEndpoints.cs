using Asp.Versioning;
using DotNetDemo.Application.Abstractions;
using DotNetDemo.Application.WeatherForecast;

namespace DotNetDemo.Api.WeatherForecast;

public static class WeatherForecastEndpoints
{
    public static IEndpointRouteBuilder MapWeatherForecastEndpoints(this IEndpointRouteBuilder app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1, 0))
            .ReportApiVersions()
            .Build();

        app.MapGet("/v{version:apiVersion}/weatherforecast", async (IQueryDispatcher dispatcher, CancellationToken cancellationToken) =>
            await dispatcher.SendAsync(new GetWeatherForecastQuery(), cancellationToken))
            .WithName("GetWeatherForecast")
            .WithApiVersionSet(versionSet);

        return app;
    }
}
