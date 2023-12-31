﻿using Weather.Application.Responses;
using Weather.Domain.Dtos;
using Weather.Domain.Dtos.OpenWeatherDtos;
using Weather.Domain.Dtos.Requests;
using Weather.Domain.Dtos.Responses;
using Weather.Domain.Entities;
using Weather.Domain.Interfaces;
using Weather.Domain.Interfaces.Repositories;
using Weather.Domain.Interfaces.Services;
using Weather.Domain.Interfaces.Services.ExternalServices;

namespace Weather.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherApiService _weatherApiService;
    private readonly IMappingService _mappingService;
    private readonly ICurrentWeatherRepository _currentWeatherRepository;
    private readonly IDailyWeatherRepository _dailyWeatherRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IWeatherGetDtoValidator _weatherGetValidator;

    public WeatherService
    (
        IWeatherApiService weatherApiService,
        IMappingService mappingService,
        ICurrentWeatherRepository currentWeatherRepository,
        ILocationRepository locationRepository,
        IDailyWeatherRepository dailyWeatherRepository,
        IWeatherGetDtoValidator weatherGetValidator)
    {
        _weatherApiService = weatherApiService;
        _mappingService = mappingService;
        _currentWeatherRepository = currentWeatherRepository;
        _locationRepository = locationRepository;
        _dailyWeatherRepository = dailyWeatherRepository;
        _weatherGetValidator = weatherGetValidator;
    }


    public async Task<GetCurrentWeatherResponse> GetCurrentWeatherAsync(WeatherGetDto weatherGet)
    {
        GetCurrentWeatherResponse response = new();
        
        weatherGet.Longitude = Math.Round(weatherGet.Longitude, 2);
        weatherGet.Latitude = Math.Round(weatherGet.Latitude, 2);
         
        try
        {
            _weatherGetValidator.Validate(weatherGet);

            var locationGetResult = (await _locationRepository.GetAllAsync(
                x => x.Latitude == weatherGet.Latitude && x.Longitude == weatherGet.Longitude)).FirstOrDefault();
            
            if (locationGetResult is null)
            {
                var weatherGetResult = await _weatherApiService.GetWeatherData(weatherGet);

                var location = await SaveCurrentWeatherLocationAsync(weatherGetResult);
                
                if (location is not null)
                {
                    var weather = await SaveCurrentWeatherAsync(weatherGetResult, location.Id);
                    response = MapCurrentWeatherDto(weather);
                }
            }
            else
            {
                var lastWeatherRecord =
                    (await _currentWeatherRepository.GetAllAsync(x =>
                        x.LocationId == locationGetResult.Id && x.IsCurrent)).MaxBy(x => x.CreatedAt);

                if (lastWeatherRecord is null || (DateTime.UtcNow - lastWeatherRecord.CreatedAt).TotalMinutes > 15)
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
        }
        catch (Exception ex)
        {
            return new GetCurrentWeatherResponse()
            {
                Exception = ex,
                Message = ex.Message,
                IsSuccess = false
            };
        }


        return response;
    }
    
    private async Task<Location?> SaveCurrentWeatherLocationAsync(WeatherOpenWeatherMapDto weather)
    {
        if (weather.Coord is null)
        {
            return null;
        }
        
        Location? location = new()
        {
            Latitude = Math.Round(weather.Coord.Lat, 2),
            Longitude = Math.Round(weather.Coord.Lon, 2),
            CityName = weather.Name,
            CityId = weather.Id,
            Country = weather.Sys?.Country
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

    private GetCurrentWeatherResponse MapCurrentWeatherDto(CurrentWeather weather)
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
        GetDailyWeatherResponse response = new();
        weatherGet.Longitude = Math.Round(weatherGet.Longitude, 2);
        weatherGet.Latitude = Math.Round(weatherGet.Latitude, 2);

        try
        {
            _weatherGetValidator.Validate(weatherGet);

            var locationGetResult = (await _locationRepository.GetAllAsync(
                x => x.Latitude == weatherGet.Latitude && x.Longitude == weatherGet.Longitude)).FirstOrDefault();


            if (locationGetResult == null)
            {
                var weatherGetResult = await _weatherApiService.GetForecastData(weatherGet);

                var location = await SaveDailyWeatherLocationAsync(weatherGetResult);
                
                if (location is not null)
                {
                    var weather = await SaveDailyWeatherAsync(weatherGetResult, location.Id);

                    response = MapDailyWeatherDto(weather);
                }

            }
            else
            {
                var lastWeatherRecordList = (await _dailyWeatherRepository.GetAllAsync(x => x.LocationId == locationGetResult.Id && !x.IsCurrent))
                    .OrderByDescending(x => x.CreatedAt).ToList();
                
                var lastWeatherRecordFirst = lastWeatherRecordList.FirstOrDefault();

                if (lastWeatherRecordFirst is null || (DateTime.UtcNow - lastWeatherRecordFirst.CreatedAt).TotalDays >= 7)
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
        }
        catch (Exception ex)
        {
            return new GetDailyWeatherResponse()
            {
                Exception = ex,
                Message = ex.Message,
                IsSuccess = false
            };
        }


        return response;
    }
    
    private async Task<Location?> SaveDailyWeatherLocationAsync(ForecastOpenWeatherMapDto weather)
    {
        if (weather.City?.Coord is null)
        {
            return null;
        }
        
        Location location = new()
        {
            Latitude = Math.Round(weather.City.Coord.Lat, 2),
            Longitude = Math.Round(weather.City.Coord.Lon, 2),
            CityName = weather.City.Name,
            CityId = weather.City.Id,
            Country = weather.City?.Country ?? string.Empty
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

    private GetDailyWeatherResponse MapDailyWeatherDto(List<DailyWeather> weather)
    {
        var weatherDto = _mappingService.Map<List<DailyWeatherDto>>(weather);

        return new GetDailyWeatherResponse()
        {
            Data = weatherDto,
            IsSuccess = true
        };
    }
}