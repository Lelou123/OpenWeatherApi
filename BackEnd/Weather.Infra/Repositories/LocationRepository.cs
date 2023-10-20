using Microsoft.EntityFrameworkCore;
using Weather.Domain.Entities;
using Weather.Domain.Interfaces.Repositories;

namespace Weather.Infra.Repositories
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {
        public LocationRepository(DbContext context) : base(context)
        {
        }
    }
}
