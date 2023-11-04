using Weather.Domain.Dtos.OpenWeatherDtos;
using Weather.Domain.Dtos.Requests;

namespace Weather.Domain.Interfaces.Services.ExternalServices;

public interface IWeatherApiService
{
    Task<ForecastOpenWeatherMapDto> GetForecastData(WeatherGetDto weatherGet);
    Task<WeatherOpenWeatherMapDto> GetWeatherData(WeatherGetDto weatherGet);
}