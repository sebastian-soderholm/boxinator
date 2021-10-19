using boxinator.Models;
using boxinator.Models.Domain;
using Microsoft.EntityFrameworkCore;
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
        public async Task<User> Verify(User user)
        {
            throw new NotImplementedException();
        }
        
        public async Task<User> Get(string email)
        {
            return await _context.Users
                .Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
