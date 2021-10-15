using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.ShipmentStatusLog
{
    public class ShipmentStatusLogCreateDTO
    {
        public int ShipmentId { get; set; }
        public StatusCreateDTO Status { get; set; }
        public DateTime Date { get; set; }
    }
}
