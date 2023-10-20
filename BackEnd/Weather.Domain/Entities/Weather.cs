namespace Weather.Domain.Entities
{
    public class Weather : BaseEntity
    {                
        public DateTime Date { get; set; }
        public int Humidity { get; set; }
        public double FeelsLike { get; set; }
        public double Temperature { get; set; }
        public double TemperatureMin { get; set; }
        public double TemperatureMax { get; set; }
        public int SeaLevel { get; set; }
        public int GroundLevel { get; set; }
        public int Pressure { get; set; }
        public int WeatherId { get; set; }
        public string WeatherMain { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIcon { get; set; }
        public double WindSpeed { get; set; }
        public int Visibility { get; set; }
        public double RainVolume { get; set; }
        public double Pop { get; set; }
        public Guid LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
