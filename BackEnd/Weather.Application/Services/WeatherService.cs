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
                    x => x.Latitude == weatherGet.Latitude &&
                    x.Longitude == weatherGet.Longitude)
                ).SingleOrDefault();


            GetCurrentWeatherResponse response = new();

            if (locationGetResult == null)
            {
                var weatherGetResult = await _weatherApiService.GetWeatherData(weatherGet);

                Location location = new()
                {
                    Latitude = weatherGetResult.Coord.Lat,
                    Longitude = weatherGetResult.Coord.Lon,
                    CityName = weatherGetResult.Name,
                    CityId = weatherGetResult.Id,
                    Country = weatherGetResult.Sys.Country
                };

                var weather = _mappingService.Map<CurrentWeather>(weatherGetResult);

                weather.LocationId = location.Id;

                await _locationRepository.InsertAsync(location);
                await _currentWeatherRepository.InsertAsync(weather);

                var weatherResponse = _mappingService.Map<CurrentWeatherDto>(weather);

                response.Data = weatherResponse;
                response.IsSuccess = true;
            }
            else
            {
                var lastWeatherRecord = (await _currentWeatherRepository.GetAllAsync(x => x.LocationId == locationGetResult.Id && x.IsCurrent))
                                                                    .OrderByDescending(x => x.CreatedAt).FirstOrDefault();

                if (lastWeatherRecord == null || (DateTime.UtcNow - lastWeatherRecord.CreatedAt).TotalMinutes > 15)
                {
                    var weatherGetResult = await _weatherApiService.GetWeatherData(weatherGet);

                    var weather = _mappingService.Map<CurrentWeather>(weatherGetResult);

                    weather.LocationId = locationGetResult.Id;

                    await _currentWeatherRepository.InsertAsync(weather);

                    var weatherResponse = _mappingService.Map<CurrentWeatherDto>(weather);
                    response.Data = weatherResponse;
                    response.IsSuccess = true;
                }
                else
                {

                    var weatherResponse = _mappingService.Map<CurrentWeatherDto>(lastWeatherRecord);
                    response.Data = weatherResponse;
                    response.IsSuccess = true;
                }
            }

            return response;
        }






        public async Task<GetDailyWeatherResponse> GetDailyWeatherAsync(WeatherGetDto weatherGet)
        {

            var locationGetResult = (await _locationRepository.GetAllAsync(
                    x => x.Latitude == weatherGet.Latitude &&
                    x.Longitude == weatherGet.Longitude)
                ).SingleOrDefault();


            GetDailyWeatherResponse response = new();

            if (locationGetResult == null)
            {
                var weatherGetResult = await _weatherApiService.GetForecastData(weatherGet);

                Location location = new()
                {
                    Latitude = weatherGetResult.City.Coord.Lat,
                    Longitude = weatherGetResult.City.Coord.Lat,
                    CityName = weatherGetResult.City.Name,
                    CityId = weatherGetResult.City.Id,
                    Country = weatherGetResult.City.Country
                };

                var weather = _mappingService.Map<DailyWeather>(weatherGetResult);

                weather.LocationId = location.Id;

                await _locationRepository.InsertAsync(location);
                await _dailyWeatherRepository.InsertAsync(weather);

                var weatherResponse = _mappingService.Map<List<DailyWeatherDto>>(weather);
                response.Data = weatherResponse;
                response.IsSuccess = true;
            }
            else
            {
                var lastWeatherRecord = (await _dailyWeatherRepository.GetAllAsync(x => x.LocationId == locationGetResult.Id && !x.IsCurrent))
                                                                    .OrderByDescending(x => x.CreatedAt).ToList();

                var lastWeatherRecordFirst = lastWeatherRecord.FirstOrDefault();
                if (lastWeatherRecordFirst == null || (DateTime.UtcNow - lastWeatherRecordFirst.CreatedAt).TotalHours > 24)
                {

                    var weatherGetResult = await _weatherApiService.GetForecastData(weatherGet);

                    var weather = _mappingService.Map<List<DailyWeather>>(weatherGetResult.List);
                    
                    foreach (var item in weather)
                    {
                        item.LocationId = locationGetResult.Id;
                    }
                    

                    await _dailyWeatherRepository.InsertRangeAsync(weather);

                    var weatherResponse = _mappingService.Map<List<DailyWeatherDto>>(weather);
                    response.Data = weatherResponse;
                    response.IsSuccess = true;

                }
                else
                {
                    var weatherResponse = _mappingService.Map<List<DailyWeatherDto>>(lastWeatherRecord);
                    response.Data = weatherResponse;
                    response.IsSuccess = true;
                }
            }

            return response;
        }


    }
}
