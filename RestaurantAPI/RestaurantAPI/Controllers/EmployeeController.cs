using DataAccess;
using DataAccess.DataTransferObjects;
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

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_employeeRepository.GetAll());
        }

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

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeDTO employee)
        {
            return Ok(_employeeRepository.Create(employee));
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] EmployeeDTO employee)
        {
            return Ok(_employeeRepository.Update(employee));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(); //TODO_employeeRepository.Delete(employee) //TODO
        }
    }
}