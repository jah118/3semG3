using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class FoodController : ControllerBase
    {
        private IRepository<FoodDTO> _foodRepository;

        public FoodController(IRepository<FoodDTO> foodRepository)
        {
            _foodRepository = foodRepository;
        }

        // GET: api/<FoodController>
        [HttpGet]
        public IActionResult Get()
        {
            var res = _foodRepository.GetAll();
            return res != null ? Ok(res) : NotFound();
        }

        // GET api/<FoodController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FoodController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            return NotFound();
        }

        // PUT api/<FoodController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] string value)
        {
            return NotFound();
        }

        // DELETE api/<FoodController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NotFound();
        }
    }
}