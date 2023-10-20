using System.ComponentModel.DataAnnotations;

namespace WeatherApi.Requests.Weather
{
    public class ForecastGetRequest
    {
        [Required]
        public double Latitude { get; set; }
        
        [Required]
        public double Longitude { get; set; }
        
        [Required] 
        public string AppId { get; set; }
        
        public string? Units { get; set; }
    }
}
