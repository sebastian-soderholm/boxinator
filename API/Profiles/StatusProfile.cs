using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Profiles
{
    public class StatusProfile : Profile
    {
        public StatusProfile()
        {
            //CreateMap<Shipment, ShipmentReadDTO>();
            //CreateMap<ShipmentCreateDTO, Shipment>();

            CreateMap<Status, StatusReadDTO>().ReverseMap();
        }
    }
}
