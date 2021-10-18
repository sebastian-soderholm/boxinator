using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Zone;

namespace boxinator.Profiles
{
    public class ZoneProfile : Profile
    {
        public ZoneProfile()
        {
            CreateMap<Zone, ZoneReadDTO>().ReverseMap();

            CreateMap<Zone, ZoneEditDTO>().ReverseMap();

            CreateMap<Zone, ZoneCreateDTO>().ReverseMap();
        }
    }
}
