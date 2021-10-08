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
        /// Get country by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retrieved country</returns>
        public async Task<Country> Get(int id) =>
            await _context.Countries.Where(x => x.Id == id).FirstOrDefaultAsync();

        /// <summary>
        /// Get all countries
        /// </summary>
        /// <returns>List of countries</returns>
        public async Task<List<Country>> GetAll() =>
            await _context.Countries.ToListAsync();

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
