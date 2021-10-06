using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentEditDTO
    {
        public string ReceiverName { get; set; }
        public double Cost { get; set; }
        public ICollection<ShipmentReadDTO> ShipmentStatusList { get; set; }

    }
}
