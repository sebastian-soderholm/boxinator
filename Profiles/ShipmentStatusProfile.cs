using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.ShipmentStatus;

namespace boxinator.Profiles
{
    public class ShipmentStatusProfile : Profile
    {
        public ShipmentStatusProfile()
        {
            CreateMap<ShipmentStatus, ShipmentStatusReadDTO>();
        }
    }
}
