using boxinator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public interface ISettingsService
    {
        public Task<Country> Get(int id);
        public Task<List<Country>> GetAll();
        public Task<Country> Add(Country country);
        public Task<Country> Update(int id, Country country);

    }
}
