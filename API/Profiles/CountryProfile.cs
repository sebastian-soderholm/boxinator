using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.Country;

namespace boxinator.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country, CountryReadDTO>()
                .ForPath(dto => dto.CountryMultiplier, opt => opt.MapFrom(c => c.Zone.CountryMultiplier))
                .ForPath(dto => dto.ZoneName, opt => opt.MapFrom(c => c.Zone.Name))
                .ReverseMap();
        }
    }
}
