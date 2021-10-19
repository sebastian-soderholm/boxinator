using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.Status;
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
        public Task<Country> GetCountry(int id);
        public Task<List<Country>> GetAllCountries();
        public Task<Country> AddCountry(CountryCreateDTO country);
        public Task<Country> UpdateCountry(int countryId, CountryEditDTO country);

        // Zones
        public Task<List<Zone>> GetAllZones();
        public Task<Zone> UpdateZone(ZoneEditDTO zone);
        public Task<Zone> AddZone(ZoneCreateDTO zone);

        // Statuses
        public Task<List<Status>> GetAllStatuses();
        public Task<Status> UpdateStatus(StatusEditDTO status);
        public Task<Status> AddStatus(StatusCreateDTO status);

    }
}
