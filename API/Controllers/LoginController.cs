using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using FirebaseAdmin;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("login")]
    [EnableCors("_myAllowSpecificOrigins")]
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
            var test = HttpContext.User.Claims;
            // NOTE! Rate limiting policy must be added

            if (userDTO == null)
                return BadRequest();

            User userinfo = _mapper.Map<User>(userDTO);
            User resultUser = await _service.Login(userinfo);

            if(resultUser == null)
                return StatusCode(401);

            return StatusCode(201);
        }

        /*
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(TokenVerifyRequest request)
        {
            var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;

            try
            {
                var response = await auth.VerifyIdTokenAsync(request.Token);
                if (response != null)
                    return Accepted();
            }
            catch (FirebaseException ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }
        */

    }
}
