using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;

namespace boxinator.Profiles
{
    public class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            CreateMap<Shipment, ShipmentReadDTO>();
            CreateMap<ShipmentReadDTO, Shipment>();
        }
    }
}
