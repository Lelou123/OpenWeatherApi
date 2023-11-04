namespace Weather.Domain.Entities;

public class Location : BaseEntity
{        
    public decimal Latitude { get; set; }        
    public decimal Longitude { get; set; }
    public string? CityName { get; set; }
    public int CityId { get; set; }
    public string? Country { get; set; }
        
    public virtual ICollection<Weather>? Weather { get; set; }
}