using DotNetDemo.Api.WeatherForecast;
using DotNetDemo.Application.WeatherForecast;
using DotNetDemo.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "DotNet Demo API";
        document.Info.Version = "v1";
        document.Info.Description = "A demo API with OpenAPI support, health checks, and API versioning";
        return Task.CompletedTask;
    });
});
builder.Services.AddHealthChecks();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});
builder.Services.AddInfrastructure(typeof(GetWeatherForecastHandler).Assembly);

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.WithTitle("DotNet Demo API")
           .WithTheme(Scalar.AspNetCore.ScalarTheme.Moon);
});

app.UseHttpsRedirection();
app.MapHealthChecks("/health");
app.MapHealthChecks("/ready");
app.MapWeatherForecastEndpoints();

app.Run();
