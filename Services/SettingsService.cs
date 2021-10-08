using boxinator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public class SettingsService : ISettingsService
    {
        public Task<Country> Add(Country country)
        {
            throw new NotImplementedException();
        }

        public Task<Country> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Country> Update(int id, Country country)
        {
            throw new NotImplementedException();
        }
    }
}
