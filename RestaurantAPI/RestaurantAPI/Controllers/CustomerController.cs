using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CustomerController : ControllerBase
    {
        private readonly IRepository<CustomerDTO> _customerRepository;

        public CustomerController(IRepository<CustomerDTO> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            return Ok(_customerRepository.GetAll());
        }

        // GET: api/Customer/5
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)   //Customer --> Get
        {
            var customer = _customerRepository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] CustomerDTO customer)
        {
            return Ok(_customerRepository.Create(customer));
        }

        // PUT: api/Customer/5
        [Authorize]
        [HttpPut]
        public IActionResult Put(int id, [FromBody] CustomerDTO customer)
        {
            return Ok(_customerRepository.Update(customer));
        }

        // DELETE: api/Customer/5
        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok("not implemented");
        }
    }
}