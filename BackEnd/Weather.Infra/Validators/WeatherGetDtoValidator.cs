using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Dtos.Requests;
using Weather.Domain.Interfaces;

namespace Weather.Infra.Validators
{
    public class WeatherGetDtoValidator : IWeatherGetDtoValidator
    {
        public void Validate(WeatherGetDto entity)
        {
            new FluentWeatherGetDtoValidator().ValidateAndThrow(entity);
        }
    }
}
