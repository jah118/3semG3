using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Authentication;
using RestaurantAPI.Models;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}"), Authorize]
        public IActionResult Get([FromHeader] string token, int id)
        {
            var res = _accountRepository.GetById(id);
            return res != null ? Ok(res) : NotFound(id);
        }

        [HttpPost()]
        public IActionResult Post([FromBody] LoginInfo user)
        {
            string res = null;
            var resulting = _accountRepository.Create(user.User);
            _authManager.Authenticate(resulting.Username, user.Password, resulting.AccountType);
            return res != null ? Ok(res) : Conflict(user.Username);
        }

        [HttpPost()]
        public IActionResult Authenticate([FromBody] LoginInfo login)
        {
            var token = _authManager.Authenticate(login.Username, login.Password, login.Role);
            return token != null ? Unauthorized() : Ok(token);
        }

    }
}