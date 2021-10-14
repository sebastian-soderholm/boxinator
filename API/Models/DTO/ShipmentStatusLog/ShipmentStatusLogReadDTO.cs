using boxinator.Models.DTO.Box;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.ShipmentStatusLog
{
    public class ShipmentStatusLogReadDTO
    {
        public StatusReadDTO StatusReadDTO { get; set; }
        public ShipmentReadDTO ShipmentReadDTO { get; set; }
        public DateTime Date { get; set; }

    }
}
