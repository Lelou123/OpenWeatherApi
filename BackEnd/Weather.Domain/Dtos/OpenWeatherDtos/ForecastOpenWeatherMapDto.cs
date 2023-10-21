namespace Weather.Domain.Dtos.OpenWeatherDtos
{
    public class ForecastOpenWeatherMapDto
    {
        public string Cod { get; set; }
        public int Message { get; set; }
        public int Cnt { get; set; }
        public List<WeathersList> List { get; set; }
        public City City { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double Feels_Like { get; set; }
        public double Temp_Min { get; set; }
        public double Temp_Max { get; set; }
        public int Pressure { get; set; }
        public int Sea_Level { get; set; }
        public int Grnd_Level { get; set; }
        public int Humidity { get; set; }
        public double Temp_Kf { get; set; }
    }


    public class WeathersList
    {
        public int Dt { get; set; }
        public Main Main { get; set; }
        public List<Weather> Weather { get; set; }
        public Clouds Clouds { get; set; }
        public Wind Wind { get; set; }
        public int Visibility { get; set; }
        public double Pop { get; set; }
        public Dictionary<string, double> Rain { get; set; }
        public Sys Sys { get; set; }
        public string DtTxt { get; set; }
    }

    

    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coord Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
        public int Timezone { get; set; }
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }

    
}
