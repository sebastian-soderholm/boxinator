using AutoMapper;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.Country;
using boxinator.Models.DTO.Status;
using boxinator.Models.DTO.Zone;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("settings")]
    [EnableCors("_myAllowSpecificOrigins")]
    //[Authorize]
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
        /// Get countries with multipliers
        /// </summary>
        /// <returns>List of countries with multipliers</returns>
        // GET: /settings/countries
        [HttpGet]
        [Route("/settings/countries")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CountryReadDTO>>> GetAllCountries()
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _accountService.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                var countries = await _settingsService.GetAllCountries();
                return _mapper.Map<List<CountryReadDTO>>(countries);
            }

            return StatusCode(403);
        }

        /// <summary>
        /// Add new country
        /// </summary>
        /// <param name="countryDTO"></param>
        /// <returns>Added country</returns>
        // POST: /settings/countries
        [HttpPost]
        [Route("/settings/countries")]
        [AllowAnonymous]
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
        /// <param name="countryId"></param>
        /// <param name="countryDTO"></param>
        /// <returns>Updated country</returns>
        //PUT: /settings/countries/:id
        [HttpPut]
        [Route("/settings/countries/{countryId}")]
        [AllowAnonymous]
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

        //GET: /settings/zones
        [HttpGet]
        [Route("/settings/zones")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ZoneReadDTO>>> GetAllZones()
        {
            var zones = await _settingsService.GetAllZones();
            return _mapper.Map<List<ZoneReadDTO>>(zones);
        }

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
        /// <param name="zoneId"></param>
        /// <param name="zoneDTO"></param>
        /// <returns></returns>
        //PUT: /settings/zones/{zoneId}
        [HttpPut]
        [Route("/settings/zones/{zoneId}")]
        [AllowAnonymous]
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
        /// Add zone
        /// </summary>
        /// <param name="zoneDTO"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        //GET: /settings/statuses
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
        /// <returns></returns>
        //PUT: /settings/statuses
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
        /// Add status
        /// </summary>
        /// <param name="statusDTO"></param>
        /// <returns></returns>
        //POST: /settings/statuses
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
