using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;
using Weather.Infra.Context;

namespace Weather.Infra.Repositories;

public class CurrentWeatherRepository : RepositoryBase<CurrentWeather>, ICurrentWeatherRepository
{
    public CurrentWeatherRepository(DbSqlServerContext context) : base(context)
    {
    }
}