using Microsoft.AspNetCore.Mvc;

namespace bankIt.Controllers
{
    [ApiController]
    [Route("Testing")]
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

        [HttpGet(Name = "WeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            //SQLDriver.cmd.CommandText = "SELECT * FROM bankIt.accounts;";
            //return SQLDriver.cmd.ExecuteReaderAsync().Result;

            String returnVal = "";

            var reader = SQLDriver.ReaderQuery("SELECT balance FROM bankIt.accounts WHERE username = \"tristan\" && password = \"tristan\";");

            while (reader.Read())
            {
                returnVal = reader.GetString(0);
            }
            

            /*var result = SQLDriver.cmd.ExecuteReaderAsync().Result;*/

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = returnVal
            })
            .ToArray();
        }
    }
}
