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

        public async Task<Shipment> Add(Shipment shipment)
        {
            var resultShipment = await _context.Shipments.AddAsync(shipment);
            _context.SaveChanges();

            return resultShipment.Entity;
        }

        public async Task<Shipment> Update(int id, Shipment shipment)
        {
            var resultShipment = await _context.Shipments
                .Include(s => s.User)
                .Where(x => x.Id == id /*&& x.UserId == currentUser*/).FirstOrDefaultAsync();

            if (resultShipment != null)
            {
                resultShipment = shipment;
                _context.SaveChanges();
            }

            return resultShipment;
        }

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

        public async Task<Shipment> Get(int id)
        {
            return await _context.Shipments
                .Include(s => s.User)
                .Where(x => x.Id == id /*&& x.UserId == currentUser*/).FirstOrDefaultAsync();
        }

        public async Task<List<Shipment>> GetByUser(int userId)
        {
            return await _context.Shipments.Where(x => x.UserId == userId).ToListAsync();
        }
        public async Task<List<ShipmentStatus>> GetAllCurrent()
        {
            return await _context.ShipmentStatuses
                .Include(s => s.Shipment)
                .ThenInclude(u => u.User)
                .Where(s => s.Status == "CURRENT" /*&& s.Shipment.UserId == currentUserId*/).ToListAsync();
        }
        public async Task<List<ShipmentStatus>> GetAllComplete()
        {
            return await _context.ShipmentStatuses
                .Include(s => s.Shipment)
                .ThenInclude(u => u.User)
                .Where(s => s.Status == "COMPLETE" /*&& s.Shipment.UserId == currentUserId*/).ToListAsync();
        }

        public async Task<List<ShipmentStatus>> GetAllCancelled()
        {
            return await _context.ShipmentStatuses
                .Include(s => s.Shipment)
                .ThenInclude(u => u.User)
                .Where(s => s.Status == "CANCELLED" /*&& s.Shipment.UserId == currentUserId*/).ToListAsync();
        }





    }
}
