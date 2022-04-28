using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecast123Controller : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecast123Controller> _logger;

        public WeatherForecast123Controller(ILogger<WeatherForecast123Controller> logger)
        {
            _logger = logger;
        }

        //[HttpGet] https://localhost:7222/WeatherForecast

        [ResponseCache(VaryByHeader = "User-Agent", Duration = 30)]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}