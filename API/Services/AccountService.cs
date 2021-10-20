﻿using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services
{
    public class AccountService : IAccountService
    {
        private readonly BoxinatorDbContext _context;

        public AccountService(BoxinatorDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Createed user</returns>
        public async Task<User> Add(User user)
        {
            var resultUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return resultUser.Entity;
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True / False</returns>
        public async Task<bool> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null /*|| user.UserId != currentUSer*/)
                return false;

            _context.Users.Remove(user);
            var rows = await _context.SaveChangesAsync();

            if (rows > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retrieved user</returns>
        public async Task<User> Get(int id)
        {
            return await _context.Users
                .Where(x => x.Id == id /*&& x.UserId == currentUser*/)
                .Include(x => x.Country)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Update user by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>Updated user</returns>
        public async Task<User> Update(int id, User user)
        {
            var resultUser = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == user.Id);

            if (resultUser != null)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return user;
        }
        /// <summary>
        /// Get user by email 
        /// </summary>
        /// <param name="email">Email as string</param>
        /// <returns>User object if found, otherwise returns null</returns>
        public async Task<User> GetUser(string email)
        {
            var resultUser = await _context.Users
                .Where(user => user.Email.Equals(email)).FirstOrDefaultAsync();

            return resultUser;
        }

        /// <summary>
        /// Search matching users by firstname, lastname, email or address
        /// </summary>
        /// <param name="term"></param>
        /// <returns>Retrieved user</returns>
        public async Task<List<User>> Search(string term)
        {
            var userList = await _context.Users
                .Where(x =>
                x.FirstName.Contains(term) || 
                x.LastName.Contains(term) ||
                x.Email.Contains(term) ||
                x.Address.Contains(term))
                .ToListAsync();
            return userList;
        }
    }
}
