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
                .ReverseMap();

            CreateMap<User, UserEditDTO>().ReverseMap();
        }
    }
}
