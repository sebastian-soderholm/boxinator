using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("login")]
    public class LoginController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public LoginController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Logins or registers user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>StatusCodes 400/401/201</returns>
        /// POST: /login
        [HttpPost]
        public async Task<IActionResult> Login(UserCreateDTO userDTO)
        {
            // NOTE! Rate limiting policy must be added

            if(userDTO == null)
                return BadRequest();

            User userinfo = _mapper.Map<User>(userDTO);
            User resultUser = await _service.Login(userinfo);

            if(resultUser == null)
                return StatusCode(401);

            return StatusCode(201);
        }

    }
}
