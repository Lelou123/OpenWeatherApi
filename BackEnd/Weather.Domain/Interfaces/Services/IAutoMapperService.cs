using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.Domain.Interfaces.Services
{
    public interface IAutoMapperService
    {
        TDestination Map<TDestination>(object source);
        IEnumerable<TDestination> MapRange<TDestination>(IEnumerable<object> source);
        void Map<TSource, TDestination>(TSource source, TDestination destination);
    }
}
