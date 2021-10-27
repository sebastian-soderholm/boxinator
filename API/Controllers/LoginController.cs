using AutoMapper;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("login")]
    [Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        public LoginController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Tries to login with email found in token. If email was not found in database, creates new user.
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Fetched user or created user</returns>
        /// GET: /login/verify
        [HttpGet("/login/verify")]
        public async Task<ActionResult<UserReadDTO>> Verify()
        {
            // Fetch user's email from token
            var userEmail = Request.ExtractEmailFromToken();
            var resultUser = await _service.GetUser(userEmail);
            // If email was found in the database, returns user
            if(resultUser != null)
            {
                return _mapper.Map<UserReadDTO>(resultUser);
            } 
            // If the email wasn't found in the database, creates a new user and returns it.
            else if(resultUser is null)
            {
                User newUser = _mapper.Map<User>(new User()
                {
                    Email = userEmail,
                    AccountType = "GUEST",
                    CountryId = 1
                });
                
                User createdUser =  await _service.Add(newUser);
                return _mapper.Map<UserReadDTO>(createdUser);
            }

            return BadRequest();
        }

    }
}
