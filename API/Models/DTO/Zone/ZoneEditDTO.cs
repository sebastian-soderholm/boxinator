using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Zone
{
    public class ZoneEditDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryMultiplier { get; set; }
    }
}
