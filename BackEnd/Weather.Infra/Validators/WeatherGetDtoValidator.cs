using FluentValidation;
using Weather.Domain.Dtos.Requests;
using Weather.Domain.Interfaces;

namespace Weather.Infra.Validators;

public class WeatherGetDtoValidator : IWeatherGetDtoValidator
{
    public void Validate(WeatherGetDto entity)
    {
        new FluentWeatherGetDtoValidator().ValidateAndThrow(entity);
    }
}