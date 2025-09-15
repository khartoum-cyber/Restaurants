using Microsoft.AspNetCore.Mvc;

namespace Restaurants.API.Controllers;

public class TemperatureRequest
{
    public int Min { get; set; }
    public int Max { get; set; }
}

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get(int resultsNumber, int minTemp, int maxTemp)
    {
        return Enumerable.Range(1, resultsNumber).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(minTemp, maxTemp),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost("generate")]
    public IActionResult Generate([FromQuery]int resultsNumber, [FromBody] TemperatureRequest request)
    {
        if (resultsNumber < 0 || request.Max < request.Min)
        {
            return BadRequest();
        }

        var result = this.Get(resultsNumber, request.Min, request.Max);

        return Ok(result);
    }
}
