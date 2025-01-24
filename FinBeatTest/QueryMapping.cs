using AutoMapper;

namespace FinBeatTest
{
    public class QueryMapping
    {
        public IMapper Mapper { get; }
        public QueryMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<KeyValuePair<string, string>, CodeValueModel>()
                .ForMember(x => x.Code, opt => opt.MapFrom(y => y.Key))
                .ForMember(x => x.Value, opt => opt.MapFrom(y => y.Value));
            });

            Mapper = config.CreateMapper();
        }
    }
}