using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.ShipmentStatusLog;

namespace boxinator.Profiles
{
    public class ShipmentStatusLogProfile : Profile
    {
        public ShipmentStatusLogProfile()
        {
            CreateMap<ShipmentStatusLog, ShipmentStatusLogReadDTO>()
                .ForMember(x => x.StatusReadDTO, opt => opt.MapFrom(x => x.Status))
                .ForMember(x => x.ShipmentReadDTO, opt => opt.MapFrom(x => x.Shipment))
                .ReverseMap();
        }
    }
}
