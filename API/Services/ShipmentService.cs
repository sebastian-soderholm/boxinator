using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.Status;
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
            //Create ShipmentStatusLog list for new shipment

            //Add status CREATED and link shipment to status
            ShipmentStatusLog shipmentStatusLog = new ShipmentStatusLog()
            {
                ShipmentId = shipment.Id,
                StatusId = (int)StatusCodes.CREATED,
                Shipment = shipment,
                Date = DateTime.Now
            };

            var resultShipmentStatusLog = await _context.ShipmentStatusLogs.AddAsync(shipmentStatusLog);
            if(resultShipmentStatusLog.Entity != null)
            {
                await _context.SaveChangesAsync();
            }

            return resultShipmentStatusLog.Entity.Shipment;
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
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).ThenInclude(ssl => ssl.Status)
                .Where(s => s.Id == id /*&& x.UserId == currentUser*/).FirstOrDefaultAsync();
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
        public async Task<List<Shipment>> GetAllCurrent()
        {
            var currentUserId = 1;

            return await _context.Shipments
                .Include(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.User)
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).ThenInclude(ssl => ssl.Status)
                .Where(t => t.ShipmentStatusLogs
                    .Any(x => x.Status.Id != (int)StatusCodes.CANCELLED || x.Status.Id != (int)StatusCodes.RECEIVED))
                .Where(u => u.UserId == currentUserId).ToListAsync();
        }

        /// <summary>
        /// Get all complete shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetAllComplete()
        {
            var currentUserId = 1;

            return await _context.Shipments
                .Include(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.User)
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).ThenInclude(ssl => ssl.Status)
                .Where(t => t.ShipmentStatusLogs.Any(x => x.Status.Id == (int)StatusCodes.RECEIVED))
                .Where(u => u.UserId == currentUserId).ToListAsync();
        }

        /// <summary>
        /// Get all cancelled shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetAllCancelled()
        {
            var currentUserId = 1;

            return await _context.Shipments
                .Include(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.User)
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).ThenInclude(ssl => ssl.Status)
                .Where(t => t.ShipmentStatusLogs.Any(x => x.Status.Id == (int)StatusCodes.CANCELLED))
                .Where(u => u.UserId == currentUserId).ToListAsync();
        }

        /// <summary>
        /// Get filtered shipments
        /// </summary>
        /// <returns>List of shipments by filters</returns>
        public async Task<List<Shipment>> GetFilteredShipments(int? statusId, DateTime? from, DateTime? to)
        {
            var currentUserId = 1;

            var query = _context.Shipments
                .Include(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.User).Where(x => x.UserId == currentUserId)
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).Where(x => x.ShipmentStatusLogs.Any(x => x.StatusId == statusId))
                .Include(s => s.ShipmentStatusLogs).Where(x => x.ShipmentStatusLogs.Any(x => x.Date > from && x.Date < to))
                .Include(s => s.ShipmentStatusLogs)
                .ThenInclude(ssl => ssl.Status)
                .AsQueryable();

            /*
            if (statusId != null)
                query.Where(u => u.User.Id == 8).AsQueryable();
                //await query.Where(x => x.ShipmentStatusLogs.Any(x => x.Status.Id == statusId)).AsQueryable().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
                //query.Where(t => t.ShipmentStatusLogs.Any(x => x.Status.Id == statusId)).AsQueryable();

            if (from != null)
                await query.Where(x => x.ShipmentStatusLogs.Any(x => x.Date > from && x.Date < to)).AsQueryable().OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            
            //query.Where(x => x.ShipmentStatusLogs.Any(x => x.Date > from && x.Date < to)).AsQueryable();
            */
            return await query.ToListAsync();
        }
    }
}
