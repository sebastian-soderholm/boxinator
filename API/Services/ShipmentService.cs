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

        public async Task<ShipmentStatusLog> AddStatusLog(int shipmentId)
        {
            var newestStatusLog = await _context.ShipmentStatusLogs.AsNoTracking()
                .Where(x => x.ShipmentId == shipmentId).OrderByDescending(x => x.Date)
                .Include(s => s.Shipment)
                .FirstOrDefaultAsync();

            // get next status
            Status nextStatus = _context.Statuses.AsNoTracking().Where(x => x.Id == (newestStatusLog.StatusId + 1)).FirstOrDefaultAsync().Result;

            if (newestStatusLog != null)
            {
                ShipmentStatusLog newLog = new ShipmentStatusLog();
                newLog.ShipmentId = newestStatusLog.ShipmentId;
                newLog.StatusId = nextStatus.Id;
                newLog.Date = DateTime.Now;

                var resultShipmentStatusLog = await _context.ShipmentStatusLogs.AddAsync(newLog);

                if (resultShipmentStatusLog.Entity != null)
                {
                    await _context.SaveChangesAsync();
                    resultShipmentStatusLog.Entity.Shipment = newestStatusLog.Shipment;
                    resultShipmentStatusLog.Entity.Status = nextStatus;
                }

                return resultShipmentStatusLog.Entity;
            }

            return null;
        }

        /// <summary>
        /// Add new shipment
        /// </summary>
        /// <param name="shipment"></param>
        /// <returns>Created shipment</returns>
        public async Task<Shipment> Add(Shipment shipment)
        {

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
        public async Task<Shipment> Update(int shipmentId, Shipment shipment)
        {
            var resultShipment = await _context.Shipments.Where(x => x.Id == shipmentId)
                .Include(u => u.User)
                .FirstOrDefaultAsync();

            if (resultShipment != null)
            {
                //resultShipment = shipment;
                resultShipment.FirstName = shipment.FirstName;
                resultShipment.LastName = shipment.LastName;
                resultShipment.ZipCode = shipment.ZipCode;
                resultShipment.Address = shipment.Address;
                resultShipment.CountryId = shipment.CountryId;
                resultShipment.User.Email = shipment.User.Email;

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
        public async Task<Shipment> Get(int id, int? currentUserId)
        {
            // find shipment by id
            Shipment resultShipment = await _context.Shipments
                .Include(s => s.User)
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).ThenInclude(ssl => ssl.Status)
                .Include(s => s.Country).ThenInclude(c => c.Zone)
                .Where(s => s.Id == id).FirstOrDefaultAsync();

            // if currentUser is not admin and if returned shipment's user doesn't match current user 
            if (currentUserId != null && resultShipment.UserId != currentUserId)
                return null;

            return resultShipment;
        }

        /// <summary>
        /// Get shipment by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetByUser(int userId)
        {
            return await _context.Shipments
                .Include(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.ShipmentStatusLogs).ThenInclude(ssl => ssl.Status)
                .Where(x => x.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// Get all current shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetAllCurrent(DateTime? from, DateTime? to, int? currentUserId)
        {
            // group statuslogs by shipmentId, get newest from each group
            var newestShipmentIds = _context.ShipmentStatusLogs.AsEnumerable()
                    .GroupBy(x => x.ShipmentId)
                    .Select(x => x.OrderByDescending(y => y.Date).Distinct().FirstOrDefault())
                    .Where(s => s.StatusId != (int)StatusCodes.CANCELLED && s.StatusId != (int)StatusCodes.COMPELED).Select(x => x.ShipmentId).ToList();

            // retrieve matching logs
            var query = _context.ShipmentStatusLogs.Where(x => newestShipmentIds.Contains(x.ShipmentId));

            // filter by dates
            if (from != null && to != null)
                query = query.Where(x => x.Date >= from && x.Date < to);

            // filter by user
            if (currentUserId != null)
                query = query.Include(s => s.Shipment).Where(u => u.Shipment.UserId == currentUserId);

            // include additional data
            query = query
                .Include(s => s.Shipment).ThenInclude(b => b.Boxes).ThenInclude(z => z.BoxType)
                .Include(s => s.Shipment).ThenInclude(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.Shipment)
                .Include(s => s.Status);

            // execute query
            var shipmentStausLogList = await query.ToListAsync();
             
            // turn list from ShipmentStatusLog to Shipment
            var endResultList = shipmentStausLogList.ToFilteredShipmentList();

            return endResultList;

        }

        /// <summary>
        /// Get all complete shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetAllComplete(DateTime? from, DateTime? to, int? currentUserId)
        {
            // set up query
            var query = _context.ShipmentStatusLogs.Where(x => x.Status.Id == (int)StatusCodes.COMPELED);

            // filter by dates
            if (from != null && to != null)
                query = query.Where(x => x.Date >= from && x.Date < to);

            // filter by user
            if (currentUserId != null)
                query = query.Include(s => s.Shipment).Where(u => u.Shipment.UserId == currentUserId);

            // include additional data
            query = query
                .Include(s => s.Shipment).ThenInclude(b => b.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.Shipment).ThenInclude(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.Shipment).Include(s => s.Status);

            // execute query
            var shipmentStausLogList = await query.ToListAsync();

            // turn list from ShipmentStatusLog to Shipment
            var endresultList = shipmentStausLogList.ToFilteredShipmentList();

            return endresultList;
        }

        /// <summary>
        /// Get all cancelled shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        public async Task<List<Shipment>> GetAllCancelled(DateTime? from, DateTime? to, int? currentUserId)
        {
            // set up query
            var query = _context.ShipmentStatusLogs.Where(x => x.Status.Id == (int)StatusCodes.CANCELLED);

            // filter by dates
            if (from != null && to != null)
                query = query.Where(x => x.Date >= from && x.Date < to);

            // filter by user
            if (currentUserId != null)
                query = query.Include(s => s.Shipment).Where(u => u.Shipment.UserId == currentUserId);

            // include additional data
            query = query
                .Include(s => s.Shipment).ThenInclude(s => s.Boxes).ThenInclude(b => b.BoxType)
                .Include(s => s.Shipment).ThenInclude(c => c.Country).ThenInclude(z => z.Zone)
                .Include(s => s.Shipment).Include(s => s.Status);

            // execute query
            var shipmentStausLogList = await query.ToListAsync();

            // turn list from ShipmentStatusLog to Shipment
            var endresultList = shipmentStausLogList.ToFilteredShipmentList();

            return endresultList;

        }

    }
}
