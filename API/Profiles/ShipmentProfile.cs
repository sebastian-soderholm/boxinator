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
                //.ForPath(dto => dto.ShipmentStatusLogs., opt => opt.MapFrom(s => s.CountryId))
                .ReverseMap();

            CreateMap<Shipment, ShipmentCreateDTO>()
                //.ForMember(dto => dto.Sender, opt => opt.MapFrom(s => s.User))
                .ForMember(dto => dto.ReveiverFirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(dto => dto.ReceiverLastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(dto => dto.ReceiverZipCode, opt => opt.MapFrom(s => s.Address))
                .ForMember(dto => dto.ReceiverAddress, opt => opt.MapFrom(s => s.ZipCode))
                .ForPath(dto => dto.SenderId, opt => opt.MapFrom(s => s.UserId))
                .ReverseMap();

        }
    }
}
