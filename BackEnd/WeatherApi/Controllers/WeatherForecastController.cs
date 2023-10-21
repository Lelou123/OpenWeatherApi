using Microsoft.AspNetCore.Mvc;
using Weather.Domain.Dtos.Requests;
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
        /// Recupera as informa��es climaticas do dia
        /// </summary>        
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso as informa��es sejam recuperadas com sucesso</response>
        [HttpGet()]
        [Route("/DailyWeather")]
        public async Task<IActionResult> GetWeatherAsync([FromQuery]WeatherGetDto request)
        {

            var response = await _weatherService.GetCurrentWeatherAsync(request);


            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }




        /// <summary>
        /// Recupera as informa��es climaticas dos proximos 5 dias
        /// </summary>        
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso as informa��es sejam recuperadas com sucesso</response>
        [HttpGet()]
        [Route("/WeeklyWeather")]
        public async Task<IActionResult> GetForecastAsync([FromQuery] WeatherGetDto request)
        {

            var response = await _weatherService.GetDailyWeatherAsync(request);

            if (!response.IsSuccess) return BadRequest(response.Message);

            return Ok(response);
        }
    }
}