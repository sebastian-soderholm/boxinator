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
                .ForMember(dto => dto.Status, opt => opt.MapFrom(s => s.Status.Name))
                .ForMember(dto => dto.ShipmentId, opt => opt.MapFrom(s => s.Shipment.Id))
                .ReverseMap();

            CreateMap<ShipmentStatusLog, ShipmentStatusLogCreateDTO>()
                .ForMember(dto => dto.Status, opt => opt.MapFrom(s => s.Status.Name))
                .ForMember(dto => dto.ShipmentId, opt => opt.MapFrom(s => s.Shipment.Id))
                .ReverseMap();
        }
    }
}
