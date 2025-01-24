using AutoMapper;
using Data.Models;
using FinBeatTest;

namespace Services.Mapping
{
    public class ServiceMapping
    {
        public IMapper Mapper { get; }
        public ServiceMapping()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataRecord, CodeValueModel>()
                .ReverseMap();
            });

            Mapper = config.CreateMapper();
        }
    }
}
