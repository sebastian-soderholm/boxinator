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
            CreateMap<Shipment, ShipmentReadDTO>()
                .ForMember(dto => dto.Sender, opt => opt.MapFrom(s => s.User))
                .ForMember(dto => dto.ReceiverFirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(dto => dto.ReceiverLastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(dto => dto.ReceiverAddress, opt => opt.MapFrom(s => s.Address))
                .ForMember(dto => dto.ReceiverZipCode, opt => opt.MapFrom(s => s.ZipCode))
                .ForPath(dto => dto.Sender.Id, opt => opt.MapFrom(s => s.UserId))
                .ForPath(dto => dto.Country.Id, opt => opt.MapFrom(s => s.CountryId))
                .ForPath(dto => dto.Country.ZoneId, opt => opt.MapFrom(s => s.Country.Zone.Id))
                .ForPath(dto => dto.Country.Name, opt => opt.MapFrom(s => s.Country.Name))
                .ForPath(dto => dto.Country.CountryMultiplier, opt => opt.MapFrom(s => s.Country.Zone.CountryMultiplier))
                .ReverseMap();

            CreateMap<Shipment, ShipmentCreateDTO>()
                //.ForMember(dto => dto.Sender, opt => opt.MapFrom(s => s.User))
                .ForMember(dto => dto.ReceiverFirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(dto => dto.ReceiverLastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(dto => dto.ReceiverZipCode, opt => opt.MapFrom(s => s.Address))
                .ForMember(dto => dto.ReceiverAddress, opt => opt.MapFrom(s => s.ZipCode))
                .ForPath(dto => dto.SenderId, opt => opt.MapFrom(s => s.UserId))
                .ForPath(dto => dto.CountryId, opt => opt.MapFrom(s => s.CountryId))
                .ReverseMap();

            CreateMap<Shipment, ShipmentGuestCreateDTO>()
                //.ForMember(dto => dto.Sender, opt => opt.MapFrom(s => s.User))
                .ForMember(dto => dto.ReceiverFirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(dto => dto.ReceiverLastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(dto => dto.ReceiverZipCode, opt => opt.MapFrom(s => s.Address))
                .ForMember(dto => dto.ReceiverAddress, opt => opt.MapFrom(s => s.ZipCode))
                .ForPath(dto => dto.CountryId, opt => opt.MapFrom(s => s.CountryId))
                .ReverseMap();

            CreateMap<Shipment, ShipmentEditDTO>()
                .ForMember(dto => dto.ReceiverFirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(dto => dto.ReceiverLastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(dto => dto.DestinationZipCode, opt => opt.MapFrom(s => s.Address))
                .ForMember(dto => dto.DestinationAddress, opt => opt.MapFrom(s => s.ZipCode))
                .ForMember(dto => dto.SenderEmail, opt => opt.MapFrom(s => s.User.Email))
                .ForPath(dto => dto.DestinationCountryId, opt => opt.MapFrom(s => s.CountryId))
                .ReverseMap();

        }
    }
}
