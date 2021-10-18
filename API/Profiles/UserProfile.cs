using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;

namespace boxinator.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>().ReverseMap();

            CreateMap<User, UserCreateDTO>()
                //.ForPath(dto => dto.Country.Id, opt => opt.MapFrom(u => u.Country.Id))
                .ReverseMap();

            CreateMap<User, UserEditDTO>().ReverseMap();
        }
    }
}
