using Weather.Application.Responses;
using Weather.Domain.Dtos.Requests;
using Weather.Domain.Dtos.Responses;

namespace Weather.Domain.Interfaces.Services;

public interface IWeatherService
{
    Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(WeatherGetDto weatherGet);
    Task<GetDailyWeatherResponse> GetDailyWeatherAsync(WeatherGetDto weatherGet);
}