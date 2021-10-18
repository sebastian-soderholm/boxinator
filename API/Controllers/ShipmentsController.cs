using AutoMapper;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("shipments")]
    [EnableCors("_myAllowSpecificOrigins")]
    //[Authorize]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _service;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public ShipmentsController(IShipmentService service, IMapper mapper, IAccountService accountService)
        {
            _service = service;
            _mapper = mapper;
            _accountService = accountService;
        }

        /// <summary>
        /// Get all current shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        // GET: /shipments
        [HttpGet]
        public async Task<ActionResult<List<ShipmentReadDTO>>> GetAllCurrent()
        {
            DateTime? parsedDateFrom = HttpContext.Request.Query["dateFromFilter"].ToString().parseDate();
            DateTime? parsedDateTo = HttpContext.Request.Query["dateToFilter"].ToString().parseDate();

            var currentShipments = await _service.GetAllCurrent(parsedDateFrom, parsedDateTo);
            var mappedList = _mapper.Map<List<ShipmentReadDTO>>(currentShipments);
            return mappedList;
        }

        /// <summary>
        /// Get all complete shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        // GET: /shipments/complete
        [HttpGet]
        [Route("/shipments/complete")]
        public async Task<ActionResult<List<ShipmentReadDTO>>> GetAllComplete()
        {
            DateTime? parsedDateFrom = HttpContext.Request.Query["dateFromFilter"].ToString().parseDate();
            DateTime? parsedDateTo = HttpContext.Request.Query["dateToFilter"].ToString().parseDate();

            var completedShipments = await _service.GetAllComplete(parsedDateFrom, parsedDateTo);
            return _mapper.Map<List<ShipmentReadDTO>>(completedShipments);
        }

        /// <summary>
        /// Get all cancelled shipments
        /// </summary>
        /// <returns>List of shipments</returns>
        // GET: /shipments/cancelled
        [HttpGet]
        [Route("/shipments/cancelled")]
        public async Task<ActionResult<List<ShipmentReadDTO>>> GetAllCancelled()
        {
            DateTime? parsedDateFrom = HttpContext.Request.Query["dateFromFilter"].ToString().parseDate();
            DateTime? parsedDateTo = HttpContext.Request.Query["dateToFilter"].ToString().parseDate();

            var cancelledShipments = await _service.GetAllCancelled(parsedDateFrom, parsedDateTo);
            return _mapper.Map<List<ShipmentReadDTO>>(cancelledShipments);
        }

        /// <summary>
        /// Add new shipment
        /// </summary>
        /// <param name="shipmentDTO"></param>
        /// <returns>Created shipment</returns>
        // POST: /shipments
        [HttpPost]
        public async Task<ActionResult<ShipmentReadDTO>> Add(ShipmentCreateDTO shipmentDTO)
        {
            Shipment newShipment = _mapper.Map<Shipment>(shipmentDTO);
            var resultShipment = await _service.Add(newShipment);
            return _mapper.Map<ShipmentReadDTO>(resultShipment);
        }

        /// <summary>
        /// Add new guest shipment
        /// </summary>
        /// <param name="shipmentGuestDTO"></param>
        /// <returns>Created shipment</returns>
        // POST: /shipments
        [HttpPost]
        [Route("/shipments/guest")]
        public async Task<ActionResult<ShipmentReadDTO>> GuestAdd(ShipmentGuestCreateDTO shipmentGuestDTO)
        {
            //Get user by email from DB 
            User userInDB = await _accountService.GetUser(shipmentGuestDTO.Email);

            //check if user does not exist in DB
            if(userInDB == null)
            {
                //Add user to DB with email, accountType, countryId
                UserCreateDTO userToDB = new UserCreateDTO();
                userToDB.Email = shipmentGuestDTO.Email;
                userToDB.AccountType = AccountTypes.GUEST.ToString();

                User newUser = _mapper.Map<User>(userToDB);
                newUser.CountryId = shipmentGuestDTO.CountryId;
                
                userInDB = await _accountService.Add(newUser);
            }

             
            Shipment newShipment = _mapper.Map<Shipment>(shipmentGuestDTO);

            //Link user to shipment and save to DB
            newShipment.User = userInDB;
            var resultShipment = await _service.Add(newShipment);
            return _mapper.Map<ShipmentReadDTO>(resultShipment);
        }

        /// <summary>
        /// Get shipment by id
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>Found shipment</returns>
        // GET: /shipments/:shipment_id
        [HttpGet("/shipments/{shipmentId}")]
        public async Task<ActionResult<ShipmentReadDTO>> Get(int shipmentId)
        {
            var shipment = await _service.Get(shipmentId);
            return _mapper.Map<ShipmentReadDTO>(shipment);
        }

        /// <summary>
        /// Get shipments by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of shipments</returns>
        // GET: /shipments/customer/:customer_id
        [HttpGet("/shipments/customer/{userId}")]
        public async Task<ActionResult<List<ShipmentReadDTO>>> GetByUser(int userId)
        {
            var shipments = await _service.GetByUser(userId);
            return _mapper.Map<List<ShipmentReadDTO>>(shipments);
        }

        /// <summary>
        /// Update shipment by id
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <param name="shipmentDto"></param>
        /// <returns>Updated shipment</returns>
        //PUT: /shipments/:shipment_id
        [HttpPut("/shipments/{shipmentId}")]
        public async Task<ActionResult<ShipmentReadDTO>> Update(int shipmentId, ShipmentEditDTO shipmentDto)
        {
            Shipment updatedShipment = _mapper.Map<Shipment>(shipmentDto);
            var resultShipment = await _service.Update(shipmentId, updatedShipment);
            return _mapper.Map<ShipmentReadDTO>(resultShipment);
        }

        /// <summary>
        /// Delete shipment by id
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>True or false</returns>
        // DELETE: /shipments/:shipment_id
        [HttpDelete("/shipments/{shipmentId}")]
        public async Task<ActionResult<bool>> Delete(int shipmentId)
        {
            return await _service.Delete(shipmentId);
        }


    }
}
