using AutoMapper;
using Com.WkTechnology.Tecnico.Service.Mapper;

namespace Com.WkTechnology.Tecnico.Test
{
    public static class MapperFactory
    {
        public static IMapper Create()
        {
            var profiles = new Profile[] { new DTOToEntityProfile() };
            var configuration = new MapperConfiguration(cfg => cfg.AddProfiles(profiles));
            return new Mapper(configuration);
        }
    }
}

