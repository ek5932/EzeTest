namespace EzeTest.Framework.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;

    public class AutoMapperObjectMapper<TSource, TDestination> : IObjectMapper<TSource, TDestination>
    {
        private readonly IMapper mapper;

        public AutoMapperObjectMapper()
        {
            var config = new MapperConfiguration(ConfigureMapping);
            config.AssertConfigurationIsValid();
            mapper = config.CreateMapper();
        }

        public TDestination Map(TSource map) => mapper.Map<TDestination>(map);

        public TDestination[] Map(IEnumerable<TSource> map) => MapCollection(map, x => Map(x));

        public TSource Map(TDestination map) => mapper.Map<TSource>(map);

        public TSource[] Map(IEnumerable<TDestination> map) => MapCollection(map, x => Map(x));

        protected virtual void ConfigureMapping(IMapperConfigurationExpression mappingConfiguration)
        {
            mappingConfiguration.CreateMap<TSource, TDestination>();
            mappingConfiguration.CreateMap<TDestination, TSource>();
        }

        private TOut[] MapCollection<TOut, TIn>(IEnumerable<TIn> toMap, Func<TIn, TOut> mapper) => 
            toMap.Select(x => mapper(x))
                 .ToArray();
    }
}
