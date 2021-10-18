using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using boxinator.Models.DTO.Zone;

namespace boxinator.Models.DTO.Country
{
    public class CountryReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
        public int CountryMultiplier { get; set; }
    }
}
