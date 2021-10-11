using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.ShipmentStatusLog;

namespace boxinator.Profiles
{
    public class ShipmentStatusLogProfile : Profile
    {
        public ShipmentStatusLogProfile()
        {
            CreateMap<ShipmentStatusLog, ShipmentStatusLogReadDTO>();
        }
    }
}
