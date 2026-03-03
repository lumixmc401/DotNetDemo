using DotNetDemo.Application.Abstractions;

namespace DotNetDemo.Application.WeatherForecast;

public record GetWeatherForecastQuery : IQuery<IEnumerable<WeatherForecastDto>>;
