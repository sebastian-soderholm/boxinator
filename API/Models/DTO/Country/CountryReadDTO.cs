﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Models.DTO.Country
{
    public class CountryReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ZoneId { get; set; }
    }
}
