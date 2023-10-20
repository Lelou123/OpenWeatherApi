using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;

namespace Weather.Infra.Repositories
{
    public class DailyWeatherRepository : RepositoryBase<DailyWeather>, IDailyWeatherRepository
    {
        public DailyWeatherRepository(DbContext context) : base(context)
        {
        }
    }
}
