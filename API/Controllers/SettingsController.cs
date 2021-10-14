using AutoMapper;
using boxinator.Models;
using boxinator.Models.DTO.Country;
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
    [Authorize]
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
            Country newCountry = _mapper.Map<Country>(countryDTO);
            Country resultCountry = await _service.Add(newCountry);
            return _mapper.Map<CountryReadDTO>(resultCountry);
        }

        /// <summary>
        /// Update country by id
        /// </summary>
        /// <param name="countryId"></param>
        /// <param name="countryDTO"></param>
        /// <returns>Updated country</returns>
        //PUT: /settings/countries:country_id
        [HttpPut]
        [Route("/settings/countries/{countryId}")]
        public async Task<ActionResult<CountryReadDTO>> Update(int countryId, CountryEditDTO countryDTO)
        {
            Country updatedCountry = _mapper.Map<Country>(countryDTO);
            Country resultCountry = await _service.Update(countryId, updatedCountry);
            return _mapper.Map<CountryReadDTO>(resultCountry);

        }
    }
}
