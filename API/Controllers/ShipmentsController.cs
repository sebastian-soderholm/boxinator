using AutoMapper;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Shipment;
using boxinator.Models.DTO.ShipmentStatusLog;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public ShipmentsController(IShipmentService service, IMapper mapper, IAccountService accountService)
        {
            _shipmentService = service;
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

            DateTime? parsedDateFrom = HttpContext.Request.Query["dateFromFilter"].ToString().ParseDate();
            DateTime? parsedDateTo = HttpContext.Request.Query["dateToFilter"].ToString().ParseDate();

            // for getting items by current user
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            // for showing admin user all shipments
            if (currentUser.IsAdmin())
                currentUser = null;

            var currentShipments = await _shipmentService.GetAllCurrent(parsedDateFrom, parsedDateTo, currentUser?.Id);
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
            DateTime? parsedDateFrom = HttpContext.Request.Query["dateFromFilter"].ToString().ParseDate();
            DateTime? parsedDateTo = HttpContext.Request.Query["dateToFilter"].ToString().ParseDate();

            // for getting items by current user
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            // for showing admin user all shipments
            if (currentUser.IsAdmin())
                currentUser = null;

            var completedShipments = await _shipmentService.GetAllComplete(parsedDateFrom, parsedDateTo, currentUser?.Id);
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
            DateTime? parsedDateFrom = HttpContext.Request.Query["dateFromFilter"].ToString().ParseDate();
            DateTime? parsedDateTo = HttpContext.Request.Query["dateToFilter"].ToString().ParseDate();

            // for getting items by current user
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            // for showing admin user all shipments
            if (currentUser.IsAdmin())
                currentUser = null;

            var cancelledShipments = await _shipmentService.GetAllCancelled(parsedDateFrom, parsedDateTo, currentUser?.Id);
            return _mapper.Map<List<ShipmentReadDTO>>(cancelledShipments);
        }

        /// <summary>
        /// Add new shipment
        /// </summary>
        /// <param name="shipmentDTO"></param>
        /// <returns>Created shipment</returns>
        // POST: /shipments
        [HttpPost]
        [Route("/shipments/")]
        public async Task<ActionResult<ShipmentReadDTO>> Add(ShipmentCreateDTO shipmentDTO)
        {
            Shipment newShipment = _mapper.Map<Shipment>(shipmentDTO);
            var resultShipment = await _shipmentService.Add(newShipment);
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
        [AllowAnonymous]
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
            var resultShipment = await _shipmentService.Add(newShipment);
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
            // for getting items by current user
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            // for showing admin user all shipments
            if (currentUser.IsAdmin())
                currentUser = null;

            var shipment = await _shipmentService.Get(shipmentId, currentUser?.Id);
            return _mapper.Map<ShipmentReadDTO>(shipment);
        }

        /// <summary>
        /// Get shipments by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of shipments or 403</returns>
        // GET: /shipments/customer/:customer_id
        [HttpGet("/shipments/customer/{userId}")]
        public async Task<ActionResult<List<ShipmentReadDTO>>> GetByUser(int userId)
        {            
            // for getting items by current user
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            // currentUser can retrieve their own shipments and admin is allowed to search for any shipment
            if(userId == currentUser.Id || currentUser.IsAdmin())
            {
                var shipments = await _shipmentService.GetByUser(userId);
                return _mapper.Map<List<ShipmentReadDTO>>(shipments);
            }

            return StatusCode(403);
        }

        /// <summary>
        /// Update shipment by id
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <param name="shipmentDto"></param>
        /// <returns>Updated shipment or 403</returns>
        //PUT: /shipments/:shipment_id
        [HttpPut("/shipments/{shipmentId}")]
        public async Task<ActionResult<ShipmentReadDTO>> Update(int shipmentId, ShipmentEditDTO shipmentDto)
        {
            // for getting items by current user
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                Shipment updatedShipment = _mapper.Map<Shipment>(shipmentDto);
                var resultShipment = await _shipmentService.Update(shipmentId, updatedShipment);
                return _mapper.Map<ShipmentReadDTO>(resultShipment);
            }

            // not authorized
            return StatusCode(403);

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
            return await _shipmentService.Delete(shipmentId);
        }

        /// <summary>
        /// Change shipments status to next one by adding new log
        /// </summary>
        /// <param name="shipmentId"></param>
        /// <returns>Newest statusLog</returns>
        /// // GET: /shipments/log/:shipment_id
        [HttpGet("/shipments/log/{shipmentId}")]
        public async Task<ActionResult<ShipmentStatusLogReadDTO>> UpdateStatus(int shipmentId)
        {
            ShipmentStatusLog updatedLog = await _shipmentService.AddStatusLog(shipmentId);
            return _mapper.Map<ShipmentStatusLogReadDTO>(updatedLog);

        }


    }
}
