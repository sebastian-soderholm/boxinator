using boxinator.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public interface IUserService 
    {
        public Task<User> Login(User user);
    }
}
