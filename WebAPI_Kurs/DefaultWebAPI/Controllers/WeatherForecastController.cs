using Microsoft.AspNetCore.Mvc;

namespace DefaultWebAPI.Controllers
{


    /*
     * 
     *  WebAPI: 
     *  
     *  - kann mit der kleinsten Konvention umgehen -> es reich ein Get/Post/Put/Delete im Methoden - Namen
     *              o   Wobei wir [HttpGet]weiterhin verwenden, weil wir hier Routing - Optionen angeboten bekommen 
     * 
     * 
     * Swagger:
     *  - Swagger orientiert sich bei den Http-Methoden Attrbiten (Get / Post / Put / Delete)  (siehese bsp) ->  [HttpGet("GetWeatherForecast123")
     */


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

        [HttpGet("GetWeatherForecast123")]
        public IEnumerable<WeatherForecast> GetWetterdaten()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}