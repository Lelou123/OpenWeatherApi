using System.ComponentModel.DataAnnotations;
using Weather.Domain.Enums;

namespace Weather.Domain.Dtos.Requests
{
    public class WeatherGetDto
    {
        [Required]
        public decimal Latitude { get; set; }

        [Required]
        public decimal Longitude { get; set; }

        public EnUnits? Units { get; set; }

        public string? Lang { get; set; }
    }
}
