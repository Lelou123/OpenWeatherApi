using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Dtos
{
    public class DailyWeatherDto : WeatherDto
    {
        public double RainVolume { get; set; }
        public double? Pop { get; set; }
        public int? SeaLevel { get; set; }
        public int? GroundLevel { get; set; }
    }
}
