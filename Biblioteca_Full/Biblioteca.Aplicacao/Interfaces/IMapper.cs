using System.Collections.Generic;

namespace Biblioteca.Aplicacao.Interfaces
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
        IEnumerable<TDestination> MapCollection<TSource, TDestination>(IEnumerable<TSource> source);
    }
}
