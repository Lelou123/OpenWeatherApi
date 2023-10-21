﻿namespace Weather.Domain.Entities
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CityName { get; set; }
        public int CityId { get; set; }
        public string Country { get; set; }
        
        public virtual ICollection<Weather> Weather { get; set; }
    }
}
