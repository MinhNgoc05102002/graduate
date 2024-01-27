using GP.Common.Helpers;
using GP.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private QuizletDbContext _quizletDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, QuizletDbContext quizletDbContext)
        {
            _logger = logger;
            _quizletDbContext = quizletDbContext;
        }

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

        [HttpGet("test")]
        public Response TestDB()
        {
            var res = _quizletDbContext.Accounts.ToList();
            return new Response(StatusCodes.Status200OK, res);
        }
    }
}