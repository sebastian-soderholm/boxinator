using boxinator.Models.DTO.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.ShipmentStatusLog
{
    public class ShipmentStatusLogReadDTO
    {
        public string Status { get; set; }
        public int StatusId { get; set; }
        public DateTime Date { get; set; }
    }
}
