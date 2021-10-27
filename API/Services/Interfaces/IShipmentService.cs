using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Services.Interfaces
{
    public interface IShipmentService
    {
        public Task<Shipment> Add(Shipment shipment);
        public Task<ShipmentStatusLog> AddStatusLog(int shipmentId);
        public Task<ShipmentStatusLog> AddCancelledStatus(int shipmentId);
        public Task<Shipment> Update(int id, Shipment shipment);
        public Task<bool> Delete(int id);
        public Task<Shipment> Get(int id, int? currentUserId);
        public Task<List<Shipment>> GetByUser(int userId);
        public Task<List<Shipment>> GetAllCurrent(DateTime? from, DateTime? to, int? currentUserId);
        public Task<List<Shipment>> GetAllComplete(DateTime? from, DateTime? to, int? currentUserId);
        public Task<List<Shipment>> GetAllCancelled(DateTime? from, DateTime? to, int? currentUserId);

    }
}
