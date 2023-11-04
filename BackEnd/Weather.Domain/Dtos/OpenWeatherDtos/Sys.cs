namespace Weather.Domain.Dtos.OpenWeatherDtos;

public class Sys
{
    public int Type { get; set; }
    public int Id { get; set; }
    public string? Country { get; set; }
    public long Sunrise { get; set; }
    public long Sunset { get; set; }
    public string? Pod { get; set; }
}