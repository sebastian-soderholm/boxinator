using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("shipments")]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _service;
        private readonly IMapper _mapper;

        public ShipmentsController(IShipmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: /shipments
        [HttpGet]
        public async Task<ActionResult<List<ShipmentStatusLogReadDTO>>> GetAllCurrent()
        {
            var currentShipments = await _service.GetAllCurrent();
            return _mapper.Map<List<ShipmentStatusLogReadDTO>>(currentShipments);
        }

        // GET: /shipments/complete
        [HttpGet]
        [Route("~/complete")]
        public async Task<ActionResult<List<ShipmentStatusLogReadDTO>>> GetAllComplete()
        {
            var completedShipments = await _service.GetAllComplete();
            return _mapper.Map<List<ShipmentStatusLogReadDTO>>(completedShipments);
        }

        // GET: /shipments/cancelled
        [HttpGet]
        [Route("~/cancelled")]
        public async Task<ActionResult<List<ShipmentStatusLogReadDTO>>> GetAllCancelled()
        {
            var cancelledShipments = await _service.GetAllCancelled();
            return _mapper.Map<List<ShipmentStatusLogReadDTO>>(cancelledShipments);
        }

        // POST: /shipments
        [HttpPost]
        public async Task<ActionResult<ShipmentReadDTO>> Add(ShipmentCreateDTO shipmentDTO)
        {
            Shipment newShipment = _mapper.Map<Shipment>(shipmentDTO);
            var resultShipment = await _service.Add(newShipment);
            return _mapper.Map<ShipmentReadDTO>(resultShipment);
        }

        // GET: /shipments/:shipment_id
        [HttpGet("{shipmentId}")]
        public async Task<ActionResult<ShipmentReadDTO>> Get(int shipmentId)
        {
            var shipment = await _service.Get(shipmentId);
            return _mapper.Map<ShipmentReadDTO>(shipment);
        }

        // GET: /shipments/customer/:customer_id
        [HttpGet("~/customer/{userId}")]
        public async Task<ActionResult<List<ShipmentReadDTO>>> GetByUser(int userId)
        {
            var shipments = await _service.GetByUser(userId);
            return _mapper.Map<List<ShipmentReadDTO>>(shipments);
        }

        //PUT: /shipments/:shipment_id
        [HttpPut("{shipmentId}")]
        public async Task<ActionResult<ShipmentReadDTO>> Update(int shipmentId, ShipmentEditDTO shipmentDto)
        {
            Shipment updatedShipment = _mapper.Map<Shipment>(shipmentDto);
            var resultShipment = await _service.Update(shipmentId, updatedShipment);
            return _mapper.Map<ShipmentReadDTO>(resultShipment);
        }

        // DELETE /shipments/:shipment_id
        [HttpDelete("{shipmentId}")]
        public async Task<ActionResult<bool>> Delete(int shipmentId)
        {
            return await _service.Delete(shipmentId);
        }

    }
}
