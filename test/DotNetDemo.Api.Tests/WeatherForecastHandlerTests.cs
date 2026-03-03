using DotNetDemo.Api.Features.WeatherForecast;

namespace DotNetDemo.Api.Tests;

public class WeatherForecastHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsWeatherForecasts()
    {
        var handler = new GetWeatherForecastHandler();

        var result = await handler.Handle(new GetWeatherForecastQuery(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(5, result.Count());
    }

    [Fact]
    public async Task Handle_ReturnsForecastsWithFutureDate()
    {
        var handler = new GetWeatherForecastHandler();
        var today = DateOnly.FromDateTime(DateTime.Now);

        var result = await handler.Handle(new GetWeatherForecastQuery(), CancellationToken.None);

        Assert.All(result, f => Assert.True(f.Date > today));
    }
}

