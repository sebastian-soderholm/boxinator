using boxinator.Models.DTO.BoxType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Box
{
    public class BoxCreateDTO
    {
        public string Color { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
