using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;

namespace boxinator.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadDTO>();
        }
    }
}
