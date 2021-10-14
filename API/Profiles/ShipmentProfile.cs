using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.Country;

namespace boxinator.Profiles
{
    public class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            CreateMap<Shipment, ShipmentReadDTO>().ReverseMap();

            CreateMap<Shipment, ShipmentCreateDTO>().ReverseMap();

        }
    }
}
