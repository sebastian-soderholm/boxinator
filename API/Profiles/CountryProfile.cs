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
                .ForMember(dto => dto.CountryMultiplier, opt => opt.MapFrom(c => c.Zone.CountryMultiplier))
                .ReverseMap();
        }
    }
}
