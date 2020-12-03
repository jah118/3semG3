using System.Net;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Authentication;
using RestaurantAPI.Filters;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthManager _authManager;

        public UserController(IAccountRepository accountRepository, IAuthManager authManager)
        {
            _accountRepository = accountRepository;
            _authManager = authManager;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _accountRepository.GetById(id);
            return res != null ? Ok(res) : NotFound(id);
        }

        [HttpGet("Testbearer"), Authorize(Roles = "Customer,Employee")]
        public IActionResult Test()
        {
            return Ok("cool");
        }

        [AllowAnonymous]
        //[EnableCors("PublicApi")]
        [HttpPost("Post")]
        public IActionResult Post([FromBody] LoginInfo user)
        {
            var resulting = _accountRepository.Create(
                user.User, user.Password);
            var token =_authManager.Authenticate(resulting.Username, user.Password, resulting.AccountType);
            return resulting != null ? Ok(token) : Conflict();
        }

        [AllowAnonymous]
        [RestrictHttps]
        [HttpPost("Authenticate")]
        //[EnableCors("PublicApi")]
        public IActionResult Authenticate([FromBody] LoginInfo login)
        {
            var token = _authManager.Authenticate(login.Username, login.Password, login.Role);
            return token == null ? Unauthorized() : Ok(token);
        }

    }
}