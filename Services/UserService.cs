using boxinator.Models;
using boxinator.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public class UserService : IUserService
    {
        private readonly BoxinatorDbContext _context;
        public UserService(BoxinatorDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Login user or register new
        /// </summary>
        /// <param name="user"></param>
        /// <returns>User</returns>
        public async Task<User> Login(User user)
        {
            // retrieve user by user email and password, if not found, register new
            throw new NotImplementedException();
        }
    }
}
