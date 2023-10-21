using Weather.Domain.Dtos;
using Weather.Domain.Interfaces;

namespace Weather.Application.Responses
{
    public class GetCurrentWeatherResponse : IResponseBase<CurrentWeatherDto>
    {
        public CurrentWeatherDto? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public Exception? Exception { get; set; }
    }
}
