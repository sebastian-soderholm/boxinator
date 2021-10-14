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
        //Receiver
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public double Cost { get; set; }
        public UserReadDTO Sender { get; set; }
        public CountryReadDTO Country { get; set; }
        public List<BoxReadDTO> Boxes { get; set; }
        public List<ShipmentStatusLogReadDTO> ShipmentStatusLogs { get; set; }

    }
}
