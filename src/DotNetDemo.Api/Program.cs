using DotNetDemo.Api.WeatherForecast;
using DotNetDemo.Application.WeatherForecast;
using DotNetDemo.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(typeof(GetWeatherForecastHandler).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapWeatherForecastEndpoints();

app.Run();
