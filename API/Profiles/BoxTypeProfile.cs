using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.BoxType;

namespace boxinator.Profiles
{
    public class BoxTypeProfile : Profile
    {
        public BoxTypeProfile()
        {
            CreateMap<BoxType, BoxTypeReadDTO>().ReverseMap();
        }
    }
}
