using boxinator.Models.DTO.Box;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Models.DTO.Status;
using boxinator.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentCreateDTO
    {
        public UserReadDTO Sender { get; set; }
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverZipCode { get; set; }
        public string ReceiverAddress { get; set; }
        public double Cost { get; set; }
        public int CountryId { get; set; }
        public List<BoxCreateDTO> Boxes { get; set; }
        public List<ShipmentStatusLogCreateDTO> ShipmentStatusLogs { get; set; }

    }
}
