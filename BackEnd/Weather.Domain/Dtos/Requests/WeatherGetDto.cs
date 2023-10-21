﻿using System.ComponentModel.DataAnnotations;

namespace Weather.Domain.Dtos.Requests
{
    public class WeatherGetDto
    {
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? Units { get; set; }

        public string? Lang { get; set; }
    }
}