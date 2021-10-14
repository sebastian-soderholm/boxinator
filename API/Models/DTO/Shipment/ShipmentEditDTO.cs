﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Shipment
{
    public class ShipmentEditDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public double Cost { get; set; }
        public ICollection<ShipmentReadDTO> ShipmentStatusLogList { get; set; }

    }
}
