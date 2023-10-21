using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Weather.Application.Responses;
using Weather.Domain.Dtos;
using Weather.Domain.Dtos.OpenWeatherDtos;
using Weather.Domain.Dtos.Requests;
using Weather.Domain.Dtos.Responses;
using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;
using Weather.Domain.Interfaces.Services;
using Weather.Domain.Interfaces.Services.ExternalServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Weather.Application.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherApiService _weatherApiService;
        private readonly IMappingService _mappingService;
        private readonly ICurrentWeatherRepository _currentWeatherRepository;
        private readonly IDailyWeatherRepository _dailyWeatherRepository;
        private readonly ILocationRepository _locationRepository;

        public WeatherService
        (
            IWeatherApiService weatherApiService,
            IMappingService mappingService,
            ICurrentWeatherRepository currentWeatherRepository,
            ILocationRepository locationRepository,
            IDailyWeatherRepository dailyWeatherRepository)
        {
            _weatherApiService = weatherApiService;
            _mappingService = mappingService;
            _currentWeatherRepository = currentWeatherRepository;
            _locationRepository = locationRepository;
            _dailyWeatherRepository = dailyWeatherRepository;
        }


        public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(WeatherGetDto weatherGet)
        {
            var locationGetResult = (await _locationRepository.GetAllAsync(
                    x => x.Latitude == weatherGet.Latitude && x.Longitude == weatherGet.Longitude)).SingleOrDefault();

            GetCurrentWeatherResponse response = new();

            if (locationGetResult == null)
            {                
                var weatherGetResult = await _weatherApiService.GetWeatherData(weatherGet);

                var location = await SaveCurrentWeatherLocationAsync(weatherGetResult);

                var weather = await SaveCurrentWeatherAsync(weatherGetResult, location.Id);

                response = MapCurrentWeatherDto(weather);
            }
            else
            {
                var lastWeatherRecord = (await _currentWeatherRepository.GetAllAsync(x => x.LocationId == locationGetResult.Id && x.IsCurrent))
                                                                    .OrderByDescending(x => x.CreatedAt).FirstOrDefault();

                if (lastWeatherRecord == null || (DateTime.UtcNow - lastWeatherRecord.CreatedAt).TotalMinutes > 15)
                {
                    var weatherGetResult = await _weatherApiService.GetWeatherData(weatherGet);

                    var weather = await SaveCurrentWeatherAsync(weatherGetResult, locationGetResult.Id);
                    
                    response = MapCurrentWeatherDto(weather);
                }
                else
                {
                    response = MapCurrentWeatherDto(lastWeatherRecord);
                }
            }

            return response;
        }
        
        

        private async Task<Location> SaveCurrentWeatherLocationAsync(WeatherOpenWeatherMapDto weather)
        {

            Location location = new()
            {
                Latitude = weather.Coord.Lat,
                Longitude = weather.Coord.Lon,
                CityName = weather.Name,
                CityId = weather.Id,
                Country = weather.Sys.Country
            };

            await _locationRepository.InsertAsync(location);

            return location;
        }

        private async Task<CurrentWeather> SaveCurrentWeatherAsync(WeatherOpenWeatherMapDto weatherGetResult, Guid locationId)
        {
            var weather = _mappingService.Map<CurrentWeather>(weatherGetResult);
            weather.LocationId = locationId;

            await _currentWeatherRepository.InsertAsync(weather);

            return weather;
        }


        public GetCurrentWeatherResponse MapCurrentWeatherDto(CurrentWeather weather)
        {
            var weatherDto = _mappingService.Map<CurrentWeatherDto>(weather);
            
            return new GetCurrentWeatherResponse()
            {
                Data = weatherDto,
                IsSuccess = true
            };
        }

        

        public async Task<GetDailyWeatherResponse> GetDailyWeatherAsync(WeatherGetDto weatherGet)
        {
            var locationGetResult = (await _locationRepository.GetAllAsync(
                    x => x.Latitude == weatherGet.Latitude && x.Longitude == weatherGet.Longitude)).SingleOrDefault();


            GetDailyWeatherResponse response = new();

            if (locationGetResult == null)
            {
                var weatherGetResult = await _weatherApiService.GetForecastData(weatherGet);

                var location = await SaveDailyWeatherLocationAsync(weatherGetResult);

                var weather = await SaveDailyWeatherAsync(weatherGetResult, location.Id);

                response = MapDailyWeatherDto(weather);
            }
            else
            {
                var lastWeatherRecordList = (await _dailyWeatherRepository.GetAllAsync(x => x.LocationId == locationGetResult.Id && !x.IsCurrent))
                                                                    .OrderByDescending(x => x.CreatedAt).ToList();

                var lastWeatherRecordFirst = lastWeatherRecordList.FirstOrDefault();

                if (lastWeatherRecordFirst == null || (DateTime.UtcNow - lastWeatherRecordFirst.CreatedAt).TotalHours > 24)
                {
                    var weatherGetResult = await _weatherApiService.GetForecastData(weatherGet);

                    var weather = await SaveDailyWeatherAsync(weatherGetResult, locationGetResult.Id);

                    response = MapDailyWeatherDto(weather);

                }
                else
                {
                    response = MapDailyWeatherDto(lastWeatherRecordList);
                }
            }

            return response;
        }


        private async Task<Location> SaveDailyWeatherLocationAsync(ForecastOpenWeatherMapDto weather)
        {

            Location location = new()
            {
                Latitude = weather.City.Coord.Lat,
                Longitude = weather.City.Coord.Lat,
                CityName = weather.City.Name,
                CityId = weather.City.Id,
                Country = weather.City.Country
            };

            await _locationRepository.InsertAsync(location);

            return location;
        }


        private async Task<List<DailyWeather>> SaveDailyWeatherAsync(ForecastOpenWeatherMapDto weatherGetResult, Guid locationId)
        {
            
            var weather = _mappingService.Map<List<DailyWeather>>(weatherGetResult.List);

            foreach (var item in weather)
            {
                item.LocationId = locationId;
            }

            await _dailyWeatherRepository.InsertRangeAsync(weather);

            return weather;
        }


        public GetDailyWeatherResponse MapDailyWeatherDto(List<DailyWeather> weather)
        {
            var weatherDto = _mappingService.Map<List<DailyWeatherDto>>(weather);

            return new GetDailyWeatherResponse()
            {
                Data = weatherDto,
                IsSuccess = true
            };
        }


    }
}
