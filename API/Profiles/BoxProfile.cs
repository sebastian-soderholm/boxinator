using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Box;

namespace boxinator.Profiles
{
    public class BoxProfile : Profile
    {
        public BoxProfile()
        {
            //Map name value
            CreateMap<Box, BoxReadDTO>()
                .ForMember(box => box.Name, opt => opt.MapFrom(x => x.BoxType.Name))
                .ForMember(box => box.Weight, opt => opt.MapFrom(x => x.BoxType.Weight))
                .ReverseMap();

            //BoxCreateDTO
            CreateMap<Box, BoxCreateDTO>()
                .ForMember(box => box.Name, opt => opt.MapFrom(x => x.BoxType.Name))
                .ForMember(box => box.Weight, opt => opt.MapFrom(x => x.BoxType.Weight))
                .ReverseMap();
        }
    }
}
