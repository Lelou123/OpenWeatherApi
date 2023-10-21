using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;
using Weather.Infra.Context;

namespace Weather.Infra.Repositories
{
    public class DailyWeatherRepository : RepositoryBase<DailyWeather>, IDailyWeatherRepository
    {
        public DailyWeatherRepository(DbPgContext context) : base(context)
        {
        }
    }
}
