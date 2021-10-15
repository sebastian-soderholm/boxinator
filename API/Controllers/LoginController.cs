using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("login")]
    [EnableCors("_myAllowSpecificOrigins")]
    //[Authorize]
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Login() // rename to Verify
        {
            //string accessTokenWithBearerPrefix = Request.Headers[HeaderNames.Authorization];
            //string accessTokenWithoutBearerPrefix = accessTokenWithBearerPrefix.Substring("Bearer ".Length);
            /*
            FirebaseApp firebaseApp = FirebaseApp.DefaultInstance;

            FirebaseAuth auth = FirebaseAuth.GetAuth(firebaseApp);
            FirebaseToken decodedToken = await auth.VerifyIdTokenAsync(accessTokenWithoutBearerPrefix);
            string uid = decodedToken.Uid;
            */
            /*
            if (AuthenticationHeaderValue.TryParse(accessTokenWithoutBearerPrefix, out var headerValue))
            {
                // we have a valid AuthenticationHeaderValue that has the following details:

                var scheme = headerValue.Scheme;
                var parameter = headerValue.Parameter;

                FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(parameter);
                string uid = decodedToken.Uid;
                // scheme will be "Bearer"
                // parmameter will be the token itself.
            }

            // NOTE! Rate limiting policy must be added
            /*
            if (userDTO == null)
                return BadRequest();
           
            User userinfo = _mapper.Map<User>(userDTO);
            User resultUser = await _service.Login(userinfo);

            if(resultUser == null)
                return StatusCode(401);
             */
            return Ok();
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
