using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ControllerBase
    {
        private readonly IRepository<UserDTO> _userRepository;

        public UserController(IRepository<UserDTO> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}"), Authorize]
        public IActionResult Get(int id)
        {

            var res = _userRepository.GetById(id);
            return res != null ? Ok(res) : NotFound(id);
        }

        [HttpPost("Register")]
        public IActionResult Post([FromBody] UserDTO value, [FromBody] string password)
        {
            var pw = password;
            var res = _userRepository.Create(value);
            
            return res != null ? Ok(res) : Conflict(value);
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] string username, [FromBody] string password)
        {
           // var token =
                return NotFound();
        }
    }
}
