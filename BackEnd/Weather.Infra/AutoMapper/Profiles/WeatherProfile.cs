using AutoMapper;
using Weather.Domain.Dtos;
using Weather.Domain.Dtos.OpenWeatherDtos;
using Weather.Domain.Entities;

namespace Weather.Infra.AutoMapper.Profiles;

public class WeatherProfile : Profile
{
    public WeatherProfile()
    {
        CreateMap<WeatherOpenWeatherMapDto, CurrentWeather>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Dt).DateTime))
            .ForMember(dest => dest.Temperature, src => src.MapFrom(s => s.Main.Temp))
            .ForMember(dest => dest.TemperatureMin, src => src.MapFrom(s => s.Main.Temp_Min))
            .ForMember(dest => dest.TemperatureMax, src => src.MapFrom(s => s.Main.Temp_Max))
            .ForMember(dest => dest.FeelsLike, src => src.MapFrom(s => s.Main.Feels_Like))
            .ForMember(dest => dest.Humidity, src => src.MapFrom(s => s.Main.Humidity))
            .ForMember(dest => dest.Pressure, src => src.MapFrom(s => s.Main.Pressure))
            .ForMember(dest => dest.WeatherId, src => src.MapFrom(s => s.Weather.Select(x => x.Id).FirstOrDefault()))
            .ForMember(dest => dest.WeatherMain, src => src.MapFrom(s => s.Weather.Select(x => x.Main).FirstOrDefault()))
            .ForMember(dest => dest.WeatherDescription, src => src.MapFrom(s => s.Weather.Select(x => x.Description).FirstOrDefault()))
            .ForMember(dest => dest.WeatherIcon, src => src.MapFrom(s => s.Weather.Select(x => x.Icon).FirstOrDefault()))
            .ForMember(dest => dest.WindSpeed, src => src.MapFrom(s => s.Wind.Speed))
            .ForMember(dest => dest.Visibility, src => src.MapFrom(s => s.Visibility))
            .ForMember(dest => dest.CloudsAll, src => src.MapFrom(s => s.Clouds.All))
            .ForMember(dest => dest.IsCurrent, src => src.MapFrom(s => true))
            .ForMember(dest => dest.Id, src => src.Ignore());
            

        CreateMap<WeathersList, DailyWeather>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Dt).DateTime))
            .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Main == null ? 0 : src.Main.Temp))
            .ForMember(dest => dest.TemperatureMin, opt => opt.MapFrom(src => src.Main == null ? 0 : src.Main.Temp_Min))
            .ForMember(dest => dest.TemperatureMax, opt => opt.MapFrom(src => src.Main.Temp_Max))
            .ForMember(dest => dest.FeelsLike, opt => opt.MapFrom(src => src.Main.Feels_Like))
            .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity))
            .ForMember(dest => dest.Pressure, opt => opt.MapFrom(src => src.Main.Pressure))
            .ForMember(dest => dest.WeatherId, opt => opt.MapFrom(src => src.Weather[0].Id))
            .ForMember(dest => dest.WeatherMain, opt => opt.MapFrom(src => src.Weather[0].Main))
            .ForMember(dest => dest.WeatherDescription, opt => opt.MapFrom(src => src.Weather[0].Description))
            .ForMember(dest => dest.WeatherIcon, opt => opt.MapFrom(src => src.Weather[0].Icon))
            .ForMember(dest => dest.WindSpeed, opt => opt.MapFrom(src => src.Wind.Speed))
            .ForMember(dest => dest.CloudsAll, opt => opt.MapFrom(src => src.Clouds.All))                
            .ForMember(dest => dest.RainVolume, opt => opt.MapFrom(src => src.Rain != null ? src.Rain["3h"] : 0))
            .ForMember(dest => dest.Pop, opt => opt.MapFrom(src => src.Pop))
            .ForMember(dest => dest.SeaLevel, opt => opt.MapFrom(src => src.Main.Sea_Level))
            .ForMember(dest => dest.IsCurrent, opt => opt.MapFrom(src => false ))
            .ForMember(dest => dest.GroundLevel, opt => opt.MapFrom(src => src.Main.Grnd_Level));


        CreateMap<CurrentWeather, CurrentWeatherDto>().ReverseMap();
        CreateMap<DailyWeather, DailyWeatherDto>().ReverseMap();

        CreateMap<Location, LocationDto>().ReverseMap();

    }
}