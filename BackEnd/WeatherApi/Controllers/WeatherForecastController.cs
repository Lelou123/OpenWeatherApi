using Microsoft.AspNetCore.Mvc;
using Weather.Domain.Interfaces.Services;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }


        /// <summary>
        /// Recupera as informa��es climaticas do momento
        /// </summary>        
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso as informa��es sejam recuperadas com sucesso</response>
        [HttpGet()]
        [Route("/WeatherForecast")]
        public async Task<IActionResult> GetWeatherForecast()
        {

            return null;
        }
    }
}