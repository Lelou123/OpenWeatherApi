using System.ComponentModel.DataAnnotations;
using Weather.Domain.Enums;

namespace Weather.Domain.Dtos.Requests
{
    public class WeatherGetDto
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public EnUnits? Units { get; set; }

        public string? Lang { get; set; }
    }
}
