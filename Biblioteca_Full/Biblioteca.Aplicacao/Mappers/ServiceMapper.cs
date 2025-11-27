using Biblioteca.Aplicacao.Interfaces;
using Mapster;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca.Aplicacao.Mappers
{
    public class ServiceMapper : IMapper
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            if (source == null) return default!;
            return source.Adapt<TDestination>();
        }

        public IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source)
        {
            if (source == null) return Enumerable.Empty<TDestination>();
            return source.Select(s => s.Adapt<TDestination>());
        }
    }
}
