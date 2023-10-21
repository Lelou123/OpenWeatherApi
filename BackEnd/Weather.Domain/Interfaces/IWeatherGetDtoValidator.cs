using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Dtos.Requests;

namespace Weather.Domain.Interfaces
{
    public interface IWeatherGetDtoValidator : IEntityValidator<WeatherGetDto>
    {
    }
}
