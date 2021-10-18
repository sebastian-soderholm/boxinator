using boxinator.Models;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Zone = boxinator.Models.Domain.Zone;

namespace boxinator.Services.Interfaces
{
    public interface ISettingsService
    {
        public Task<Country> Get(int id);
        public Task<List<Country>> GetAll();
        public Task<Country> Add(CountryCreateDTO country);
        public Task<Country> Update(CountryEditDTO country);
        public Task<List<Zone>> GetAllZones();
        public Task<Zone> UpdateZone(ZoneEditDTO zone);
        public Task<Zone> AddZone(ZoneCreateDTO zone);

    }
}
