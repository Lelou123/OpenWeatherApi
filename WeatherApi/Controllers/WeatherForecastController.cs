using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        public WeatherForecastController()
        {
           
        }

        [HttpGet()]
        [Route("/WeatherForecast")]
        public async Task<IActionResult>GetWeatherForecast()
        {
            return null;
        }
    }
}