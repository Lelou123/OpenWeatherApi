namespace Weather.Domain.Entities;

public class DailyWeather : Weather
{
    public double RainVolume { get; set; }
    public double? Pop { get; set; }
    public int? SeaLevel { get; set; }
    public int? GroundLevel { get; set; }
}