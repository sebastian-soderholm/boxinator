using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly BoxinatorDbContext _context;
        public ShipmentService(BoxinatorDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add new shipment
        /// </summary>
        /// <param name="shipment"></param>
        /// <returns>Created shipment</returns>
        public async Task<Shipment> Add(Shipment shipment)
        {
            var resultShipment = await _context.Shipments.AddAsync(shipment);
            if(resultShipment.Entity != null)
            {
                await _context.SaveChangesAsync();
            }

            return resultShipment.Entity;
        }

        /// <summary>
        /// Update existing shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="shipment"></param>
        /// <returns>Updated shipment</returns>
        public async Task<Shipment> Update(int id, Shipment shipment)
        {
            var resultShipment = await _context.Shipments
                .Include(s => s.User)
                .Where(x => x.Id == id /*&& x.UserId == currentUser*/).FirstOrDefaultAsync();

            if (resultShipment != null)
            {
                resultShipment = shipment;
                await _context.SaveChangesAsync();
            }

            return resultShipment;
        }

        /// <summary>
        /// Delete shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>True / false</returns>
        public async Task<bool> Delete(int id)
        {
            var shipment = await _context.Shipments.FindAsync(id);

            if(shipment == null /*|| shipment.UserId != currentUSer*/)
                return false;

            _context.Shipments.Remove(shipment);
            var rows = await _context.SaveChangesAsync();

            if (rows > 0)
                return true;
                
            return false;
        }

        /// <summary>
        /// Get shipment by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retrieved shipment</returns>
        public async Task<Shipment> Get(int id)
        {
            return await _context.Shipments
                .Include(s => s.User)
                .Where(x => x.Id == id /*&& x.UserId == currentUser*/).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get shipment by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetByUser(int userId)
        {
            return await _context.Shipments.Where(x => x.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Get all current shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<ShipmentStatusLog>> GetAllCurrent()
        {
            return await _context.ShipmentStatusLogs
                .Include(x => x.Status)
                .Include(s => s.Shipment)
                    .ThenInclude(x => x.Country)
                .Include(s => s.Shipment)
                    .ThenInclude(x => x.Boxes)
                .Include(s => s.Shipment)
                    .ThenInclude(u => u.User)
                .Where(s => s.StatusId != 3 && s.StatusId != 2 /*&& s.Shipment.UserId == currentUserId*/).ToListAsync();
        }

        /// <summary>
        /// Get all complete shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<ShipmentStatusLog>> GetAllComplete()
        {
            return await _context.ShipmentStatusLogs
                .Include(s => s.Shipment)
                .ThenInclude(u => u.User)
                .Where(s => s.StatusId == 1 /*&& s.Shipment.UserId == currentUserId*/).ToListAsync();
        }

        /// <summary>
        /// Get all cancelled shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<ShipmentStatusLog>> GetAllCancelled()
        {
            return await _context.ShipmentStatusLogs
                .Include(s => s.Shipment)
                .ThenInclude(u => u.User)
                .Where(s => s.StatusId == 1 /*&& s.Shipment.UserId == currentUserId*/).ToListAsync();
        }

    }
}
