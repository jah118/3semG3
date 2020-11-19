using DataAccess;
using DataAccess.DataTransferObjects;
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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = _userRepository.GetById(id);
            return res != null ? Ok(res) : NotFound(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO value, [FromHeader] string pass)
        {
            var pw = pass;
            var res = _userRepository.Create(value);
            return res != null ? Ok(res) : Conflict(value);
        }


    }
}
