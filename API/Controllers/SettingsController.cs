using AutoMapper;
using boxinator.Models;
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
    //[Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _service;
        private readonly IMapper _mapper;

        public SettingsController(ISettingsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get countries with multipliers
        /// </summary>
        /// <returns>List of countries with multipliers</returns>
        // GET: /settings/countries
        [HttpGet]
        [Route("/settings/countries")]
        public async Task<ActionResult<List<CountryReadDTO>>> GetAll()
        {
            var countries = await _service.GetAll();
            return _mapper.Map<List<CountryReadDTO>>(countries);
        }

        /// <summary>
        /// Add new country
        /// </summary>
        /// <param name="countryDTO"></param>
        /// <returns>Added country</returns>
        // POST: /settings/countries
        [HttpPost]
        [Route("/settings/countries")]
        public async Task<ActionResult<CountryReadDTO>> Add(CountryCreateDTO countryDTO)
        {
           
            var resultCountry = await _service.Add(countryDTO);
            return _mapper.Map<CountryReadDTO>(resultCountry);
        }

        /// <summary>
        /// Update country by id
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="countryDTO"></param>
        /// <returns>Updated country</returns>
        //PUT: /settings/countries
        [HttpPut]
        [Route("/settings/countries")]
        public async Task<ActionResult<CountryReadDTO>> Update(CountryEditDTO countryDTO)
        {
            var resultCountry = await _service.Update(countryDTO);
            return _mapper.Map<CountryReadDTO>(resultCountry);

        }
        /// <summary>
        /// Get all countries with zone info
        /// </summary>
        /// <returns>List of countries with multipliers</returns>
        //GET: /settings/zones
        [HttpGet]
        [Route("/settings/zones")]
        public async Task<ActionResult<List<ZoneReadDTO>>> GetAllZones()
        {
            var zones = await _service.GetAllZones();
            return _mapper.Map<List<ZoneReadDTO>>(zones);
        }

        /// <summary>
        /// Edit zone info
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="zoneDTO"></param>
        /// <returns></returns>
        //PUT: /settings/zones
        [HttpPut]
        [Route("/settings/zones")]
        public async Task<ActionResult<ZoneReadDTO>> UpdateZone(ZoneEditDTO zoneDTO)
        {
            var resultZone = await _service.UpdateZone(zoneDTO);
            return _mapper.Map<ZoneReadDTO>(resultZone);
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
            var resultZone = await _service.AddZone(zoneDTO);
            return _mapper.Map<ZoneReadDTO>(resultZone);
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
            var resultStatuses = await _service.GetAllStatuses();
            return _mapper.Map<List<StatusReadDTO>>(resultStatuses);
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
            var resultStatus = await _service.UpdateStatus(statusDTO);
            return _mapper.Map<StatusReadDTO>(resultStatus);
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
            var resultStatus = await _service.AddStatus(statusDTO);
            return _mapper.Map<StatusReadDTO>(resultStatus);
        }
    }
}
