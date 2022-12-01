using Houthakkers.Servies;
using Microsoft.AspNetCore.Mvc;

namespace Houthakkers.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly MijnServies _servies;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, MijnServies servies)
    {
        _logger = logger;
        _servies = servies;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogCritical("Kritiek");
        _logger.LogError("Error");
        _logger.LogWarning("Warning");
        _logger.LogInformation("Information");
        _logger.LogDebug("Debug");
        _logger.LogTrace("Verbose");
        _servies.Increment();
        
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
