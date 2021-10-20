using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.Zone;
using Zone = boxinator.Models.Domain.Zone;
using boxinator.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using boxinator.Models.DTO.Status;

namespace boxinator.Services.Interfaces
{
    public class SettingsService : ISettingsService
    {
        private readonly BoxinatorDbContext _context;
        private readonly IMapper _mapper;

        public SettingsService(BoxinatorDbContext context, IMapper mapper)
        {
            
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Add new country
        /// </summary>
        /// <param name="country"></param>
        /// <returns>Created country</returns>
        public async Task<Country> AddCountry(CountryCreateDTO countryDTO)
        {
            var zoneFromDB = await _context.Zones.AsNoTracking().FirstOrDefaultAsync(z => z.Id == countryDTO.ZoneId);

            if(zoneFromDB != null)
            {
                Country country = _mapper.Map<Country>(countryDTO);
                await _context.Countries.AddAsync(country);
                await _context.SaveChangesAsync();
                return country;
            }

            return null;
        }

        /// <summary>
        /// Get country with multiplier by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retrieved country</returns>
        public async Task<Country> GetCountry(int id)
        {
            Country country = await _context.Countries.Where(x => x.Id == id)
            .Include(x => x.Zone)
            .FirstOrDefaultAsync();

            return country;
        }
            

        /// <summary>
        /// Get all countries and multipliers
        /// </summary>
        /// <returns>List of countries</returns>
        public async Task<List<Country>> GetAllCountries() =>
            await _context.Countries.Include(x => x.Zone).ToListAsync();

        /// <summary>
        /// Update country by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="country"></param>
        /// <returns>Updated country</returns>
        
        public async Task<Country> UpdateCountry(int countryId, CountryEditDTO countryDTO)
        {
            Country country = _mapper.Map<Country>(countryDTO);

            var countryFromDB = await _context.Countries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == countryId);
            if(countryFromDB != null)
            {
                //Get Zone from DB by given zoneId
                var zone = await _context.Zones.AsNoTracking().FirstOrDefaultAsync(z => z.Id == countryDTO.ZoneId);

                //Set Zone to country if found, else return null
                if (zone != null) country.Zone = zone;
                else return null;

                _context.Update(country);
                await _context.SaveChangesAsync();
            }

            return country;
        }

        /// <summary>
        /// Get all countries and multipliers
        /// </summary>
        /// <returns>List of countries</returns>
        public async Task<List<Zone>> GetAllZones() =>
            await _context.Zones.ToListAsync();

        /// <summary>
        /// Get all countries for a zone id
        /// </summary>
        /// <param name="zoneId"></param>
        /// <returns></returns>
        public async Task<List<Country>> GetZoneCountries(int zoneId) =>
            await _context.Countries.Where(c => c.ZoneId == zoneId).ToListAsync();

        /// <summary>
        /// Update Zone info
        /// </summary>
        /// <param name="id"></param>
        /// <param name="zoneDTO"></param>
        /// <returns></returns>
        public async Task<Zone> UpdateZone(ZoneEditDTO zoneDTO)
        {
            var zoneFromDB = await _context.Zones.AsNoTracking().FirstOrDefaultAsync(z => z.Id == zoneDTO.Id);

            if (zoneFromDB != null)
            {
                Zone zone = _mapper.Map<Zone>(zoneDTO);
                _context.Update(zone);
                await _context.SaveChangesAsync();
                return zone;
            }

            return null;
        }
        /// <summary>
        /// Add Zone
        /// </summary>
        /// <param name="zoneDTO"></param>
        /// <returns></returns>
        public async Task<Zone> AddZone(ZoneCreateDTO zoneDTO)
        {
            Zone zone = _mapper.Map<Zone>(zoneDTO);
            await _context.Zones.AddAsync(zone);
            await _context.SaveChangesAsync();
            return zone;
        }

        public async Task<List<Status>> GetAllStatuses() =>
            await _context.Statuses.ToListAsync();

        public async Task<Status> UpdateStatus(StatusEditDTO statusDTO)
        {
            var statusFromDB = await _context.Statuses.AsNoTracking().FirstOrDefaultAsync(s => s.Id == statusDTO.Id);

            if (statusFromDB != null)
            {
                Status status = _mapper.Map<Status>(statusDTO);
                _context.Update(status);
                await _context.SaveChangesAsync();
                return status;
            }

            return null;
        }

        public async Task<Status> AddStatus(StatusCreateDTO statusDTO)
        {
            Status status = _mapper.Map<Status>(statusDTO);
            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();
            return status;
        }
    }
}
