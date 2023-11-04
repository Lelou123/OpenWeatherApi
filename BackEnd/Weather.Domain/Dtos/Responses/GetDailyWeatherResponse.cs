using Weather.Domain.Interfaces;

namespace Weather.Domain.Dtos.Responses;

public class GetDailyWeatherResponse : IResponseBase<List<DailyWeatherDto>>
{
    public List<DailyWeatherDto>? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public Exception? Exception { get; set; }
}