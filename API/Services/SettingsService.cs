using boxinator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public class SettingsService : ISettingsService
    {
        private readonly BoxinatorDbContext _context;

        public SettingsService(BoxinatorDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new country
        /// </summary>
        /// <param name="country"></param>
        /// <returns>Created country</returns>
        public async Task<Country> Add(Country country)
        {
            var resultCountry = await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();

            return resultCountry.Entity;
        }

        /// <summary>
        /// Get country with multiplier by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retrieved country</returns>
        public async Task<Country> Get(int id) =>
            await _context.Countries.Where(x => x.Id == id)
            .Include(x => x.Zone)
            .FirstOrDefaultAsync();

        /// <summary>
        /// Get all countries and multipliers
        /// </summary>
        /// <returns>List of countries</returns>
        public async Task<List<Country>> GetAll() =>
            await _context.Countries.Include(x => x.Zone).ToListAsync();

        /// <summary>
        /// Update country by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="country"></param>
        /// <returns>Updated country</returns>
        public async Task<Country> Update(int id, Country country)
        {
            var resultCountry = await _context.Countries.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(resultCountry != null)
            {
                resultCountry = country;
                await _context.SaveChangesAsync();
            }

            return resultCountry;
        }
    }
}
