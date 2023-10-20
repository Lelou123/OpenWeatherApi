using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;

namespace Weather.Infra.Repositories
{
    public class CurrentWeatherRepository : RepositoryBase<CurrentWeather>, ICurrentWeatherRepository
    {
        public CurrentWeatherRepository(DbContext context) : base(context)
        {
        }
    }
}
