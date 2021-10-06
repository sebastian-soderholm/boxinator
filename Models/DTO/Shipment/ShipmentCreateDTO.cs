using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentCreateDTO
    {
        public string ReceiverName { get; set; }
        public double Cost { get; set; }
        public ICollection<ShipmentCreateDTO> ShipmentStatusList { get; set; }

    }
}
