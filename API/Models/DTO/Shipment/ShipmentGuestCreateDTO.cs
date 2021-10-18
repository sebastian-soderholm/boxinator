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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public double Cost { get; set; }
        public CountryReadDTO Country { get; set; }
        public List<BoxCreateDTO> Boxes { get; set; }
    }
}
