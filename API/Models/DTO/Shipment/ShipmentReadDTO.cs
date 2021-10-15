using boxinator.Models.DTO.Box;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Models.DTO.Status;
using boxinator.Models.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentReadDTO
    {
        public int Id { get; set; }        
        public string ReveiverFirstName { get; set; }
        public string ReveiverLastName { get; set; }
        public string ReveiverAddress { get; set; }
        public string ReveiverZipCode { get; set; }
        public double Cost { get; set; }
        public UserReadDTO Sender { get; set; }
        public CountryReadDTO Country { get; set; }
        public ICollection<BoxReadDTO> Boxes { get; set; }
        public ICollection<ShipmentStatusLogReadDTO> ShipmentStatusLogs { get; set; }

    }
}
