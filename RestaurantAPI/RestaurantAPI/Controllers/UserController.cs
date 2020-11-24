using DataAccess;
using DataAccess.DataTransferObjects;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Authentication;

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
        public IActionResult Get(int id)
        {

            var res = _accountRepository.GetById(id);
            return res != null ? Ok(res) : NotFound(id);
        }

        [HttpPost("Register")]
        public IActionResult Post([FromBody] UserDTO value, [FromBody] string password)
        {
            string res = null;
            var resulting = _accountRepository.Create(value);
            _authManager.Authenticate(resulting.Username, password, resulting.AccountType);
            return res != null ? Ok(res) : Conflict(value.Username);
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] string username, [FromBody] string password, [FromBody] UserRoles role)
        {
            var token = _authManager.Authenticate(username, password, role);
            return token != null ? Unauthorized() : Ok(token);
        }
    }
}
