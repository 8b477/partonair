using API.partonair.AutoMapperConfiguration.GenericType;

using AutoMapper;

namespace API.partonair.AutoMapperConfiguration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper Init()
        {
                var config = new MapperConfiguration(cfg =>
            cfg.CreateMap(typeof(Source<>), typeof(Destination<>)));

            return config.CreateMapper();
        }
    }
}
