using MediatR;

namespace DotNetDemo.Api.Features.WeatherForecast;

public record GetWeatherForecastQuery : IRequest<IEnumerable<WeatherForecastDto>>;

public record WeatherForecastDto(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC * 9.0 / 5.0);
}
