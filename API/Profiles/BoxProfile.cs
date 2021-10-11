using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Box;

namespace boxinator.Profiles
{
    public class BoxProfile : Profile
    {
        public BoxProfile()
        {
            CreateMap<Box, BoxReadDTO>();
        }
    }
}
