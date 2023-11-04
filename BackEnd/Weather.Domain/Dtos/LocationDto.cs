namespace Weather.Domain.Dtos;

public class LocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? CityName { get; set; }
    public int CityId { get; set; }
    public string? Country { get; set; }
}