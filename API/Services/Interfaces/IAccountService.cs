using boxinator.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<User> Get(int id);
        public Task<User> Update(int id, User user);
        public Task<User> Add(User user);
        public Task<bool> Delete(int id);
        public Task<User> GetUser(string email);
        public Task<List<User>> Search(string term);

    }
}
