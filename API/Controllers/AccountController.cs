using AutoMapper;
using boxinator.Models.Domain;
using boxinator.Models.DTO.User;
using boxinator.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace boxinator.Controllers
{
    [ApiController]
    [Route("account")]
    //[Authorize]
    [EnableCors("_myAllowSpecificOrigins")]
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
        /// <param name="accountId"></param>
        /// <returns>Retrieved user</returns>
        // GET: /account/:account_id
        [HttpGet("/account/{accountId}")]
        public async Task<ActionResult<UserReadDTO>> Get(int accountId)
        {
            var resultUser = await _service.Get(accountId);
            return _mapper.Map<UserReadDTO>(resultUser);
        }

        /// <summary>
        /// Update specific user
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="userDTO"></param>
        /// <returns>Updated user</returns>
        // PUT: /account/:account_id
        [HttpPut("/account/{accountId}")]
        public async Task<ActionResult<UserReadDTO>> Update(int accountId, UserEditDTO userDTO)
        {
            User updatedUser = _mapper.Map<User>(userDTO);
            User resultUser = await _service.Update(accountId, updatedUser);
            return _mapper.Map<UserReadDTO>(resultUser);
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="userDTO"></param>
        /// <returns></returns>
        // POST: /account
        [HttpPost]
        public async Task<ActionResult<UserReadDTO>> Add(UserCreateDTO userDTO)
        {
            User newUser = _mapper.Map<User>(userDTO);
            User resultUser = await _service.Add(newUser);
            return _mapper.Map<UserReadDTO>(resultUser);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        // DELETE: /account/:account_id
        [HttpDelete("/account/{accountId}")]
        public async Task<ActionResult<bool>> Delete(int accountId)
        {
            // RESTRICT TO ADMIN ONLY!
            return await _service.Delete(accountId);
        }
    }
}
