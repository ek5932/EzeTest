namespace EzeTest.Framework.Mapping
{
    using System.Collections.Generic;

    public interface IObjectMapper<TSource, TDestination>
    {
        TSource Map(TDestination map);

        TDestination Map(TSource map);

        TSource[] Map(IEnumerable<TDestination> map);

        TDestination[] Map(IEnumerable<TSource> map);
    }
}
