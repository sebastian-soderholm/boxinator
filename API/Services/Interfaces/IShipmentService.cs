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
        public Task<Shipment> Update(int id, Shipment shipment);
        public Task<bool> Delete(int id);
        public Task<Shipment> Get(int id);
        public Task<List<Shipment>> GetByUser(int userId);
        public Task<List<ShipmentStatusLog>> GetAllCurrent();
        public Task<List<Shipment>> GetAllComplete();
        public Task<List<ShipmentStatusLog>> GetAllCancelled();


    }
}
