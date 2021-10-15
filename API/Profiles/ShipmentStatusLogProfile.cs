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
                .ForMember(dto => dto.Status, opt => opt.MapFrom(s => s.Status))
                .ForPath(dto => dto.ShipmentId, opt => opt.MapFrom(s => s.Shipment.Id))
                .ReverseMap();

            CreateMap<ShipmentStatusLog, ShipmentStatusLogCreateDTO>()
                .ForPath(dto => dto.Status, opt => opt.MapFrom(s => s.Status))
                .ForPath(dto => dto.ShipmentId, opt => opt.MapFrom(s => s.Shipment.Id))
                .ReverseMap();
        }
    }
}
