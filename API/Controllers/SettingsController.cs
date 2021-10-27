using AutoMapper;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.Status;
using boxinator.Models.DTO.User;
using boxinator.Models.DTO.Zone;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("settings")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [EnableCors("_myAllowSpecificOrigins")]
    [Authorize]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsService settingsService, IMapper mapper, IAccountService accountService)
        {
            _settingsService = settingsService;
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Update specific user by id
        /// </summary>
        /// <param name="accountId">Users id</param>
        /// <param name="userDTO"></param>
        /// <returns>Updated user or 403</returns>
        // PUT: /settings/account/:account_id
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPut("/settings/account/{accountId}")]
        public async Task<ActionResult<UserReadDTO>> Update(int accountId, UserEditDTO userDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                User updatedUser = _mapper.Map<User>(userDTO);
                User resultUser = await _accountService.Update(accountId, updatedUser);
                return _mapper.Map<UserReadDTO>(resultUser);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Get all countries with multipliers
        /// </summary>
        /// <returns>List of countries with multipliers</returns>
        // GET: /settings/countries
        [HttpGet]
        [Route("/settings/countries")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CountryReadDTO>>> GetAllCountries()
        {
            var countries = await _settingsService.GetAllCountries();
            return _mapper.Map<List<CountryReadDTO>>(countries);
        }

        /// <summary>
        /// Add new country
        /// </summary>
        /// <param name="countryDTO"></param>
        /// <returns>Added country or 403</returns>
        // POST: /settings/countries
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPost]
        [Route("/settings/countries")]
        public async Task<ActionResult<CountryReadDTO>> AddCountry(CountryCreateDTO countryDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var resultCountry = await _settingsService.AddCountry(countryDTO);
                return _mapper.Map<CountryReadDTO>(resultCountry);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Update country by id
        /// </summary>
        /// <param name="countryId">Country's id</param>
        /// <param name="countryDTO"></param>
        /// <returns>Updated country or 403</returns>
        //PUT: /settings/countries/:id
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPut]
        [Route("/settings/countries/{countryId}")]
        public async Task<ActionResult<CountryReadDTO>> UpdateCountry(int countryId, CountryEditDTO countryDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var resultCountry = await _settingsService.UpdateCountry(countryId, countryDTO);
                return _mapper.Map<CountryReadDTO>(resultCountry);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Gets all the zones
        /// </summary>
        /// <returns>List of zones</returns>
        //GET: /settings/zones
        [HttpGet]
        [Route("/settings/zones")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ZoneReadDTO>>> GetAllZones()
        {
            var zones = await _settingsService.GetAllZones();
            return _mapper.Map<List<ZoneReadDTO>>(zones);
        }

        /// <summary>
        /// Gets all countrys in zone by id
        /// </summary>
        /// <param name="zoneId">Zone's id</param>
        /// <returns>List of countries in zone</returns>
        //GET: /settings/zones/{zoneId}
        [HttpGet("/settings/zones/{zoneId}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CountryReadDTO>>> GetZonesCountries(int zoneId)
        {
            var zones = await _settingsService.GetZoneCountries(zoneId);
            return _mapper.Map<List<CountryReadDTO>>(zones);
        }

        /// <summary>
        /// Edit zone info
        /// </summary>
        /// <param name="zoneId">Zone's id</param>
        /// <param name="zoneDTO"></param>
        /// <returns>Updated zone or 403</returns>
        //PUT: /settings/zones/{zoneId}
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPut]
        [Route("/settings/zones/{zoneId}")]
        public async Task<ActionResult<ZoneReadDTO>> UpdateZone(int zoneId, ZoneEditDTO zoneDTO)
        {            
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var resultZone = await _settingsService.UpdateZone(zoneId, zoneDTO);
                return _mapper.Map<ZoneReadDTO>(resultZone);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Add a new zone
        /// </summary>
        /// <param name="zoneDTO"></param>
        /// <returns>New zone or 403</returns>
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPost]
        [Route("/settings/zones")]
        public async Task<ActionResult<ZoneReadDTO>> Add(ZoneCreateDTO zoneDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var resultZone = await _settingsService.AddZone(zoneDTO);
                return _mapper.Map<ZoneReadDTO>(resultZone);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Get all statuses
        /// </summary>
        /// <returns>List of statuses or 403</returns>
        //GET: /settings/statuses
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpGet]
        [Route("/settings/statuses")]
        public async Task<ActionResult<List<StatusReadDTO>>> GetAllStatuses()
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var resultStatuses = await _settingsService.GetAllStatuses();
                return _mapper.Map<List<StatusReadDTO>>(resultStatuses);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Update existing status
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns>Updated status or 403</returns>
        //PUT: /settings/statuses
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPut]
        [Route("/settings/statuses")]
        public async Task<ActionResult<StatusReadDTO>> UpdateStatus(StatusEditDTO statusDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var resultStatus = await _settingsService.UpdateStatus(statusDTO);
                return _mapper.Map<StatusReadDTO>(resultStatus);
            }
            // not authorized
            return StatusCode(403);
        }

        /// <summary>
        /// Add a new status
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns>New status or 403</returns>
        //POST: /settings/statuses
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPost]
        [Route("/settings/statuses")]
        public async Task<ActionResult<StatusReadDTO>> AddStatus(StatusCreateDTO statusDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if(currentUser.IsAdmin())
            {
                var resultStatus = await _settingsService.AddStatus(statusDTO);
                return _mapper.Map<StatusReadDTO>(resultStatus);
            }
            // not authorized
            return StatusCode(403);
        }
    }
}
