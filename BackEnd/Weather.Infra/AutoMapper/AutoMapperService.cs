using AutoMapper;
using Weather.Domain.Interfaces.Services;

namespace Weather.Infra.AutoMapper
{
    public class AutoMapperService : IAutoMapperService
    {
        private readonly IMapper _mapper;

        public AutoMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            if(source == null)
            {
                throw new Exception("Source cannot be null");
            }

            return _mapper.Map<TDestination>(source);
        }

        public void Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            _mapper.Map(source, destination);
        }

        public IEnumerable<TDestination> MapRange<TDestination>(IEnumerable<object> source)
        {
            if (source == null)
            {
                throw new Exception("Source cannot be null");
            }

            return _mapper.Map<IEnumerable<TDestination>>(source);
        }
    }
}
