using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;
using Weather.Infra.Context;

namespace Weather.Infra.Repositories
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(DbPgContext context) : base(context)
        {
        }
    }
}
