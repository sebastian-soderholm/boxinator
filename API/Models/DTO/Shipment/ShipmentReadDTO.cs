using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentReadDTO
    {
        public string ReceiverName { get; set; }
        public double Cost { get; set; }
        public ICollection<ShipmentReadDTO> ShipmentStatusLogList { get; set; }

    }
}
