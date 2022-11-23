using Microsoft.AspNetCore.Mvc;

namespace api.infomanager.dk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        public object Index()
        {
            return new
            {
                DateTime = DateTime.Now,
                Title = "Testing multible get's in same controller"
            };
        }

        [HttpGet("WeatherForecast/{id}", Name = "Retrieve")]
        public object Retrieve(int id)
        {
            return new
            {
                DateTime = DateTime.Now,
                Title = "Specific WeatherForecasts",
                Id = id
            };
        }

        [HttpGet("Overview", Name = "Overview")]
        public object Overview()
        {
            return new
            {
                DateTime = DateTime.Now,
                Title = "Overview of WeatherForecasts"
            };
        }
    }
}