using boxinator.Models.DTO.Box;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.ShipmentStatusLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentGuestCreateDTO
    {
        //Guest email
        public string Email { get; set; }
        //Shipment reciever info
        public string ReceiverFirstName { get; set; }
        public string ReceiverLastName { get; set; }
        public string ReceiverZipCode { get; set; }
        public string ReceiverAddress { get; set; }
        public double Cost { get; set; }
        public int CountryId { get; set; }
        public List<BoxCreateDTO> Boxes { get; set; }
    }
}
