using DataAccess;
using DataAccess.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository<EmployeeDTO> _employeeRepository;

        public EmployeeController(IRepository<EmployeeDTO> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeRepository.GetAll());
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] EmployeeDTO employee)
        {
            return Ok(_employeeRepository.Create(employee));
        }

        [Authorize]
        [HttpPut]
        public IActionResult Put(int id, [FromBody] EmployeeDTO employee)
        {
            return Ok(_employeeRepository.Update(employee));
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(); //TODO_employeeRepository.Delete(employee) //TODO
        }
    }
}