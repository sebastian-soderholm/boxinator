using AutoMapper;
using boxinator.Models;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("account")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [EnableCors("_myAllowSpecificOrigins")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        public AccountController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get user information by id
        /// </summary>
        /// <param name="accountId">Users Id</param>
        /// <returns>Retrieved user, new created user or 403</returns>
        // GET: /account/:account_id
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpGet("/account/{accountId}")]
        public async Task<ActionResult<UserReadDTO>> Get(int accountId)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _service.GetUser(userEmail);

            if (currentUser.IsAdmin() || currentUser.Id == accountId)
            {
                var resultUser = await _service.Get(accountId);
                return _mapper.Map<UserReadDTO>(resultUser);
            }

            return StatusCode(403);
        }

        /// <summary>
        /// Update specific user by id
        /// </summary>
        /// <param name="accountId">Users id</param>
        /// <param name="userDTO"></param>
        /// <returns>Updated user or 403</returns>
        // PUT: /account/:account_id
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPut("/account/{accountId}")]
        public async Task<ActionResult<UserReadDTO>> Update(int accountId, UserEditDTO userDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _service.GetUser(userEmail);

            if( currentUser.Id == accountId)
            {
                User updatedUser = _mapper.Map<User>(userDTO);
                User resultUser = await _service.Update(accountId, updatedUser);
                return _mapper.Map<UserReadDTO>(resultUser);
            }

            return StatusCode(403);
        }

        /// <summary>
        /// Update specific user after registration
        /// </summary>
        /// <param name="accountId">Users id</param>
        /// <param name="userDTO"></param>
        /// <returns>Updated user or 403</returns>
        // PUT: /account/register/:account_id
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPut("/account/register/{accountId}")]
        public async Task<ActionResult<UserReadDTO>> Register(int accountId, UserEditDTO userDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _service.GetUser(userEmail);

            if (currentUser.Id == accountId)
            {
                User updatedUser = _mapper.Map<User>(userDTO);
                User resultUser = await _service.Register(accountId, updatedUser);
                return _mapper.Map<UserReadDTO>(resultUser);
            }

            return StatusCode(403);
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns>Added user or 403</returns>
        // POST: /account
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> Add(UserCreateDTO userDTO)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _service.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                User newUser = _mapper.Map<User>(userDTO);
                User resultUser = await _service.Add(newUser);
                return _mapper.Map<UserReadDTO>(resultUser);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>200, 404 or 403</returns>
        // DELETE: /account/:account_id
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
        [HttpDelete("/account/{accountId}")]
        public async Task<ActionResult> Delete(int accountId)
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _service.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                bool success = await _service.Delete(accountId);
                return success == true ? Ok() : StatusCode(404);
            }
            return StatusCode(403);
        }

        /// <summary>
        /// Search user
        /// </summary>
        /// <returns>Found user or 403</returns>
        [ProducesResponseType(Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]
        [HttpGet]
        public async Task<ActionResult<List<UserReadDTO>>> Search()
        {
            // current user for role check
            var userEmail = Request.ExtractEmailFromToken();
            User currentUser = await _service.GetUser(userEmail);

            if (currentUser.IsAdmin())
            {
                string searchTerm = HttpContext.Request.Query["searchTerm"].ToString();

                var resultUsers = await _service.Search(searchTerm);
                return _mapper.Map<List<UserReadDTO>>(resultUsers);
            }
            return StatusCode(403);
        }
    }
}
